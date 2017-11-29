using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2A
{
    class WasteCodeDump
    {
        /*
                            Console.WriteLine("\nPlease input value {0}" , i);
                            HeatInput = Convert.ToChar(Console.ReadLine());
                            if (HeatInput == 'h')
                            {
                                InputOrder[i - 1] = 0;
                            }
                            if (HeatInput == 'w')
                            {
                                InputOrder[i - 1] = 1;
                            }
                            if (HeatInput == 'c')
                            {
                                InputOrder[i - 1] = 2;
                            }
                            if (HeatInput == 'f')
                            {
                                InputOrder[i - 1] = 3;
                            }
                            else
                            {
                                InputOrder[i] = 0;
                            }
                            */

        /*
                            Console.WriteLine("\nPlease input value {0}", i); ;
                            HeatInput = Convert.ToChar(Console.ReadLine());
                            if (HeatInput == 'h')
                            {
                                InputOrder[i - 1] = 0;
                            }
                            if (HeatInput == 'w')
                            {
                                InputOrder[i - 1] = 1;
                            }
                            if (HeatInput == 'c')
                            {
                                InputOrder[i - 1] = 2;
                            }
                            if (HeatInput == 'f')
                            {
                                InputOrder[i - 1] = 3;
                            }
                            */


        /*
                            Console.WriteLine("\nPlease input value {0}", i); ;
                            HeatInput = Convert.ToChar(Console.ReadLine());
                            if (HeatInput == 'h')
                            {
                                InputOrder[i - 1] = 0;
                            }
                            if (HeatInput == 'w')
                            {
                                InputOrder[i - 1] = 1;
                            }
                            if (HeatInput == 'c')
                            {
                                InputOrder[i - 1] = 2;
                            }
                            if (HeatInput == 'f')
                            {
                                InputOrder[i - 1] = 3;
                            }
                            */

        // first attemp at probability result

        //firstStateOff = ((OffHeat[InputOrder[j]] * OffToOn[0]) / (OffToOn[0])) * startState[0];
        //firstStateOn = ((OnHeat[InputOrder[j]]* OnToOff[0]) /(OnToOff[0]))*startState[0]; // OnHeat[InputOrder[j]]


        /*
                        *
                        *for (j = 0; i < 4; j++)
                           {
                           if (j == 0)
                           {
                               if (inputOrder[j] == 0 || inputOrder[j] == 1)
                               {
                                   answerStorage[0] = ((onOffChange[0] * startState[0]) + (onOffChange[1] * startState[1]));
                                   answerStorage[1] = ((onOffChange[2] * startState[0]) + (onOffChange[3] * startState[1]));

                                   answerNumbers[0] = ((onHeat[0] * answerStorage[0]) + (onHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((onHeat[2] * answerStorage[0]) + (onHeat[3] * answerStorage[1]));
                               }
                               else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                               {
                                   answerStorage[0] = ((onOffChange[0] * startState[0]) + (onOffChange[1] * startState[1]));
                                   answerStorage[1] = ((onOffChange[2] * startState[0]) + (onOffChange[3] * startState[1]));

                                   answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));
                               }
                           }
                           if (j == 1)
                           {
                               if (inputOrder[j] == 0 || inputOrder[j] == 1)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((onHeat[0] * answerStorage[0]) + (onHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((onHeat[2] * answerStorage[0]) + (onHeat[3] * answerStorage[1]));
                               }
                               else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));
                               }
                           }
                           if (j == 2)
                           {
                               if (inputOrder[j] == 0 || inputOrder[j] == 1)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((onHeat[0] * answerStorage[0]) + (onHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((onHeat[2] * answerStorage[0]) + (onHeat[3] * answerStorage[1]));
                               }
                               else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));
                               }
                           }
                           if (j == 3)
                           {
                               if (inputOrder[j] == 0 || inputOrder[j] == 1)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((onHeat[0] * answerStorage[0]) + (onHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((onHeat[2] * answerStorage[0]) + (onHeat[3] * answerStorage[1]));
                               }
                               else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                               {
                                   answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                   answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                   answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                   answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));
                               }
                           }
                       }// end of for loop

                       FinalSumTotal = answerNumbers[0] + answerNumbers[1];
                        * 
                        */

        //Console.WriteLine("\nThe probability of the inputting sequence is {0}\n", FinalSumTotal);
        //for (i = 0; i < 4; i++)
        //{
        //    Console.WriteLine("{0}", inputOrder[i]);
        //}
    }
}
