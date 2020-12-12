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

        bool loopFlag;
        int loopCount;
        int loopSize;
        int loopIter;
        bool executeFlag = true;

        public List<KeyValuePair<string, int>> variables = new List<KeyValuePair<string, int>>();

        public void parseCommand(String line, Canvas myCanvas, Canvas pointer)
        {
            line = line.ToLower().Trim();
            string[] splitLine = line.Split();

            if (!executeFlag)
            {
                if (line == "endif")
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
                        case "endif":
                            throw new ApplicationException("No if statement");
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
                        string val = splitLine[2];

                        if (var == variables.Find(x => x.Key == var).Key)
                        {
                            var newVal = new KeyValuePair<string, int>(var, int.Parse(val));
                            variables.RemoveAll(x => x.Key == var);
                            variables.Add(newVal);
                        }
                        else { throw new ApplicationException("Unknown variable"); }
                    }

                    if (splitLine[0] == "if")
                    {
                        int i = line.IndexOf(" ") + 1;
                        string cond = line.Replace("(", " ").Replace(")", " ").Substring(i).Trim();
                        string[] splitCond = cond.Split();


                        if (splitCond.Length == 3 && splitCond[1] == "==")
                        {
                            int[] condInt = CheckVar(splitCond).Where(str => str.All(Char.IsDigit)).Select(str => int.Parse(str)).ToArray();

                            if (condInt[0] != condInt[1])
                            {
                                executeFlag = false;
                            }
                        }
                    }
                }
            }

            string[] CheckVar(string[] parameters)
            {
                string[] newParams = new string[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] == variables.Find(x => x.Key == parameters[i]).Key)
                    {
                        newParams[i] = variables.Find(x => x.Key == parameters[i]).Value.ToString();
                    } 
                    else //if (parameters[i].All(Char.IsDigit))
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
