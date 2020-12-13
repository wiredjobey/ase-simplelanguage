using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    public class Parser
    {
        public int programCount;
        public int programCountSave;

        bool loopFlag;
        int loopCount;
        int loopSize;
        int loopIter;

        public bool executeFlag = true;
        public bool methodFlag;
        bool methodAssignFlag;
        string currentMethod;

        public List<KeyValuePair<string, int>> variables = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, int>> methods = new List<KeyValuePair<string, int>>();
        List<KeyValuePair<string, int>> localMethodVars = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, KeyValuePair<string, int>>> methodVars = new List<KeyValuePair<string, KeyValuePair<string, int>>>();

        public void parseCommand(String line, Canvas myCanvas, Canvas pointer)
        {
            line = line.ToLower().Trim();
            string[] splitLine = line.Split();

            if (!executeFlag)
            {
                if (line == "endif")
                    executeFlag = true;

                if (line == "endmethod")
                    executeFlag = true;

                programCount++;
                return;
            }

            if (splitLine.Length == 2)
            {
                string command = splitLine[0];
                string[] parameters = splitLine[1].Split(',');

                ApplicationException invalidLengthException = new ApplicationException("Invalid number of parameters for " + command);


                if (command != "var")
                {
                    parameters = CheckVar(parameters);
                }

                string p = string.Join(",", parameters);

                if (!p.Any(char.IsDigit))
                {
                    switch (command)
                    {
                        case "pen":
                            if(parameters.Length == 1)
                            {
                                Color c = Color.FromName(parameters[0]);

                                if (c.IsKnownColor)
                                {
                                    myCanvas.Colour(parameters[0]);
                                }
                                else
                                {
                                    throw new ApplicationException("Invalid colour for pen");
                                }
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "fill":
                            if(parameters.Length == 1)
                            {
                                if (parameters[0] == "on")
                                {
                                    myCanvas.PenFill(true);
                                } 
                                else if (parameters[0] == "off")
                                {
                                    myCanvas.PenFill(false);
                                } else
                                {
                                    throw new ApplicationException("Invalid parameter for fill (must be on/off)");
                                }
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "var":
                            if (parameters.Length == 1)
                            {
                                variables.Add(new KeyValuePair<string, int>(parameters[0], 0));
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "call":
                            if (parameters.Length == 1)
                            {
                                string methodName = parameters[0];

                                if (methodName == methods.Find(x => x.Key == methodName).Key)
                                {
                                    programCount = methods.Find(x => x.Key == methodName).Value;
                                    methodFlag = true;
                                    methodAssignFlag = true;
                                }
                                else { throw new ApplicationException("Invalid method"); }

                            }
                            else { throw invalidLengthException; }
                            break;
                        case "moveto":
                        case "drawto":
                        case "triangle":
                        case "square":
                        case "rectangle":
                        case "circle":
                        case "loop":
                            throw new ApplicationException("Invalid parameter type (must be int)");
                        default:
                            throw new ApplicationException("Invalid command");
                    }
                } 
                else
                {
                    int[] paramsInt = new int[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        paramsInt[i] = int.Parse(parameters[i]);
                    }

                    switch (command)
                    {
                        case "moveto":
                            if (paramsInt.Length == 2)
                            {

                                myCanvas.MoveTo(paramsInt[0], paramsInt[1]);
                                pointer.MoveTo(paramsInt[0], paramsInt[1]);
                                pointer.Clear();
                                pointer.DrawRectangle(1, 1);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "drawto":
                            if (paramsInt.Length == 2)
                            {
                                myCanvas.DrawLine(paramsInt[0], paramsInt[1]);
                                pointer.MoveTo(paramsInt[0], paramsInt[1]);
                                pointer.Clear();
                                pointer.DrawRectangle(1, 1);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "triangle":
                        case "tri":
                            if (paramsInt.Length == 4)
                            {
                                myCanvas.DrawTriangle(paramsInt[0], paramsInt[1], paramsInt[2], paramsInt[3]);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "square":
                            if (paramsInt.Length == 1)
                            {
                                myCanvas.DrawRectangle(paramsInt[0], paramsInt[0]);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "rectangle":
                        case "rect":
                            if (paramsInt.Length == 2)
                            {
                                myCanvas.DrawRectangle(paramsInt[0], paramsInt[1]);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "circle":
                            if (paramsInt.Length == 1)
                            {
                                myCanvas.DrawCircle(paramsInt[0]);
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "loop":
                            if (paramsInt.Length == 1)
                            {
                                loopIter = paramsInt[0];
                                loopFlag = true;
                                loopCount = 0;
                                loopSize = 0;
                            }
                            else { throw invalidLengthException; }
                            break;
                        case "var":
                        case "pen":
                        case "fill":
                            throw new ApplicationException("Invalid parameter type (must be string)");
                        default:
                            throw new ApplicationException("Invalid command");
                    }
                }
            }
            else
            {
                if (splitLine.Length == 1)
                {
                    switch (line)
                    {
                        case "clear":
                            myCanvas.Clear();
                            break;
                        case "reset":
                            myCanvas.MoveTo(0, 0);
                            pointer.MoveTo(0, 0);
                            pointer.Clear();
                            pointer.DrawRectangle(1, 1);
                            break;
                        case "endloop":
                            loopFlag = false;
                            if (loopCount++ < loopIter)
                            {
                                programCount = programCount - loopSize;
                            }
                            break;
                        case "endmethod":
                            if (methodFlag)
                            {
                                localMethodVars.Clear();
                                methodFlag = false;
                                programCount = programCountSave;
                            }
                            else { throw new ApplicationException("Method was not started"); }

                            break;
                        case "endif":
                            break;
                        default:
                            throw new ApplicationException("Invalid command");
                    }

                }
                else
                {
                    if (splitLine[1] == "=")
                    {
                        if (splitLine.Length != 3)
                            throw new ApplicationException("Invalid number of parameters for variable assignment");

                        string var = splitLine[0];
                        int val;
                        int tryParseInt;

                        if (methodAssignFlag)
                        {
                            currentMethod = methods.Where(x => x.Value < programCount).OrderBy(x => x.Value - programCount).First().Key;
                            localMethodVars = methodVars.Where(x => x.Key == currentMethod).Select(x => x.Value).ToList();
                            methodAssignFlag = false;
                        }

                        if (int.TryParse(splitLine[2], out tryParseInt))
                            val = tryParseInt;
                        else
                            val = int.Parse(CheckVar(splitLine)[2]);

                        

                        if (methodFlag)
                        {

                            if (var == localMethodVars.Find(x => x.Key == var).Key)
                            {
                                var newVal = new KeyValuePair<string, int>(var, val);
                                localMethodVars.RemoveAll(x => x.Key == var);
                                localMethodVars.Add(newVal);

                                //var saveVal = new KeyValuePair<string, KeyValuePair<string, int>>(currentMethod, newVal);
                                //methodVars.Add(saveVal);
                            }
                        }

                        if (var == variables.Find(x => x.Key == var).Key)
                        {
                            var newVal = new KeyValuePair<string, int>(var, val);
                            variables.RemoveAll(x => x.Key == var);
                            variables.Add(newVal);
                        }
                    }

                    if (splitLine[0] == "if")
                    {
                        int removeCmd = line.IndexOf(" ") + 1;
                        string cond = line.Replace("(", " ").Replace(")", " ").Substring(removeCmd).Trim();
                        string[] splitCond = cond.Split();

                        if (splitCond.Length == 3 && splitCond[1] == "==")
                        {
                            int[] condInt = CheckVar(splitCond).Where(str => str.All(Char.IsDigit)).Select(str => int.Parse(str)).ToArray();

                            if (condInt[0] != condInt[1])
                            {
                                executeFlag = false;
                            }
                        }
                        else { throw new ApplicationException("Invalid if condition"); }
                    }

                    if (splitLine[0] == "method")
                    {
                        int removeCmd = line.IndexOf(" ") + 1;
                        string method = line.Replace("(", "").Replace(")", "").Substring(removeCmd).Trim();
                        string[] splitMethod = method.Trim().Split();

                        string methodName = splitMethod[0];
                        string[] localVars = splitMethod.Skip(1).ToArray();

                        var newMethod = new KeyValuePair<string, int>(methodName, programCount);
                        methods.Add(newMethod);

                        if (localVars.Length > 0)
                        {
                            for (int i = 0; i < localVars.Length; i++)
                            {
                                KeyValuePair<string, int> newVar = new KeyValuePair<string, int>(localVars[i], 0);
                                methodVars.Add(new KeyValuePair<string, KeyValuePair<string, int>>(methodName, newVar));
                            }
                        }

                        executeFlag = false;
                    }
                }
            }

            string[] CheckVar(string[] parameters)
            {
                string[] newParams = new string[parameters.Length];

                List<KeyValuePair<string, int>> kvp;

                if (methodFlag)
                    kvp = localMethodVars;
                else
                    kvp = variables;

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == kvp.Find(x => x.Key == parameters[i]).Key)
                    {
                        newParams[i] = kvp.Find(x => x.Key == parameters[i]).Value.ToString();
                    } 
                    else if (methodFlag)
                    {
                        methodFlag = false;
                        newParams[i] = CheckVar(parameters)[i];
                        methodFlag = true;
                    }
                    else
                    {
                        newParams[i] = parameters[i];
                    }
                }

                return newParams;
            }

            if (loopFlag)
                loopSize++;

            programCount++;
        }
    }
}
