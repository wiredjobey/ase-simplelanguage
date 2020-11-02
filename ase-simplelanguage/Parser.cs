using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Parser
    {
        void parseCommand(String line)
        {
            line = line.ToLower().Trim();
            string[] splitLine = line.Split();

            if (splitLine.Length == 2)
            {
                string command = splitLine[0];
                string[] parameters = splitLine[1].Split(',');

                int[] paramsInt = new int[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    paramsInt[i] = int.Parse(parameters[i]);
                }

                switch (command)
                {
                    case "drawto":
                        break;
                }
            } 
        }
    }
}
