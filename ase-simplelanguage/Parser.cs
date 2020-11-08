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
        public void parseCommand(String line, Canvas myCanvas, Canvas pointer)
        {
            line = line.ToLower().Trim();
            string[] splitLine = line.Split();

            if (splitLine.Length == 2)
            {
                string command = splitLine[0];
                string[] parameters = splitLine[1].Split(',');

                ApplicationException invalidLengthException = new ApplicationException("Invalid number of parameters for " + command);

                if (!splitLine[1].Any(char.IsDigit))
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
                        case "moveto":
                        case "drawto":
                        case "triangle":
                        case "square":
                        case "rectangle":
                        case "circle":
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
                switch(line)
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
                    default:
                        throw new ApplicationException("Invalid command");
                }
            }
        }
    }
}
