using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Parser
    {
        public void parseCommand(String line, Canvas MyCanvas)
        {
            line = line.ToLower().Trim();
            string[] splitLine = line.Split();

            if (splitLine.Length == 2)
            {
                string command = splitLine[0];
                string[] parameters = splitLine[1].Split(',');

                if (!splitLine[1].Any(char.IsDigit))
                {
                    switch (command)
                    {
                        case "pen":
                            if(parameters.Length == 1)
                            {
                                MyCanvas.Colour(parameters[0]);
                            }
                            break;
                        case "fill":
                            if(parameters.Length == 1)
                            {
                                if (parameters[0] == "on")
                                {
                                    MyCanvas.PenFill(true);
                                } else if (parameters[0] == "off")
                                {
                                    MyCanvas.PenFill(false);
                                }
                            }
                            break;
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
                                MyCanvas.MoveTo(paramsInt[0], paramsInt[1]);
                            }
                            break;
                        case "drawto":
                            if (paramsInt.Length == 2)
                            {
                                MyCanvas.DrawLine(paramsInt[0], paramsInt[1]);
                            }
                            break;
                        case "triangle":
                            if (paramsInt.Length == 3)
                            {
                                MyCanvas.DrawTriangle(paramsInt[0], paramsInt[1], paramsInt[2]);
                            }
                            break;
                        case "square":
                            if (paramsInt.Length == 1)
                            {
                                MyCanvas.DrawRectangle(paramsInt[0], paramsInt[0]);
                            }
                            break;
                        case "circle":
                            if (paramsInt.Length == 1)
                            {
                                MyCanvas.DrawCircle(paramsInt[0]);
                            }
                            break;
                    }
                }
            }
            else
            {
                switch(line)
                {
                    case "clear":
                        MyCanvas.Clear();
                        break;
                    case "reset":
                        MyCanvas.MoveTo(0, 0);
                        break;
                }
            }
        }
    }
}
