using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name:     Ryan Wooton - 13394001
*  Date:     28/10/2016
 *  
 * Changes: 25/11/2016 - Program complete
 * 
 *          02/12/2016 - implemented failsafe for option 1.
*
*  Info:    The purpose of this programme is to compute the the most likely
*           sequence of hidden states from a Hidden Markov Model, given
*           a sequence of outputs
*/
namespace Assignment_2B
{
    class Program
    {
        static void Main(string[] args)
        {
            int optionNum = 0,
                i,
                j,
                k;
            double finalSumTotal = 0;
            bool programmeRun = true;
            string userInput;
            double[] startState = { 0.5, 0.5 };
            // [0] to stay the same, [1] to change.
            double[] onOffChange = { 0.75, 0.25, 0.25, 0.75 };
            // [0] & [1] for X0 = ON, [2] & [3] for X0 = OFF.

            //                  X0 (current state)
            //                        ON      OFF
            //          X^T-1   ON   0.75     0.25
            //   (last state)   OFF  0.25     0.75

            // Sequence goes hot, warm, cold, freezing

            double[] onHeat = { 0.45, 0.05 };
            double[] offHeat = { 0.05, 0.45 };
            double[] answerOnStorage = { 0, 0, 0, 0 };
            double[] answerOffStorage = { 0, 0, 0, 0 };
            double[] alphaNumbers = { 0, 0 };
            double[] totalNumbers = { 0, 0 };
            double[] answerNumbers = { 0, 0 };
            int[] inputOrder = { 0, 0, 0, 0 };
            string[] outputOrder = { "", "", "", "" };

            while (programmeRun == true)
            {
                // Option one provides the user with a number of options to choose from.
                while (optionNum == 0)
                {
                    Console.WriteLine("Please press (1) to input your variables, (2) to view probability of observing results, press (3) to exit program");
                    userInput = Console.ReadLine();
                    Console.WriteLine("\n");

                    // If the inputted option is not recognised, the program triggers a failsafe and
                    // reloads the menu.
                    if (userInput != "1" & userInput != "2" & userInput != "3")
                    {
                        Console.WriteLine("\nThe value you entered was not an aceeptable \n");
                        optionNum = 4;
                    }
                    else
                    {
                        optionNum = Convert.ToInt32(userInput);
                    }
                }

                // option 1 is used to allow the user to enter their values into the program.
                while (optionNum == 1)
                {
                    Console.WriteLine("Type: \n 'h' for hot.\n 'w' for warm.\n 'c' for cold.\n 'f' for freezing.");
                    // This for loop cycles through 4 times, each time calling the 'HeatValue'
                    // method.
                    for (i = 1; i < 5; i++)
                    {
                        if (i == 1)
                        {
                            HeatValue(i, inputOrder, optionNum);
                        }
                        if (i == 2)
                        {
                            HeatValue(i, inputOrder, optionNum);
                        }
                        if (i == 3)
                        {
                            HeatValue(i, inputOrder, optionNum);
                        }
                        if (i == 4)
                        {
                            HeatValue(i, inputOrder, optionNum);
                        }

                        // This change to the FinalSumTotal allows option 2 to run prooperly
                        // once enough values have been inputted.
                        finalSumTotal = 1;
                        optionNum = 0;
                    }
                }

                // Option 2 is used to both calculate and print the answer.
                while (optionNum == 2)
                {
                    // If option 2 is selected before any values have been inputted, the program will throw
                    // the error message below.
                    if (finalSumTotal == 0)
                    {
                        Console.WriteLine("\nThere is no total to show\n");
                        optionNum = 4;
                    }
                    if (finalSumTotal != 0)
                    {
                        // Here the program loops through the entirety of the inputOrder array, given that
                        // there is an answer to compute.
                        for (j = 0; j < 4; j++)
                        {
                            // For each value of the inputOrder array, the program will first deduce whether
                            // the input is either hot/warm or cold/freezing, regardless of the result the program will
                            // begin a recursive algorithm that, for the first sum, will multiply the start state of the
                            // radiator by all of the possible transitional probabilities of the radiator to get four results,
                            // these results are then split into pairs, with the program finding the largest value for the
                            // first pair and the second pair and multiplying them both by values that change depending on
                            // the detected input, with the values being the change propability given that it's on if it detects
                            // 'hot' or 'warm', and the change probability given that it's off if it detects 'cold' or
                            // 'freezing', regardless of the input, the alpha for these values are then computed by taking the
                            // calculated values, adding them together to get a total and then dividing this total by each of
                            // the original calculated values, the two derived values are then used to determin whether
                            // 'ON' or 'OFF' is more likely.
                            if (j == 0)
                            {
                                if (inputOrder[j] == 0 || inputOrder[j] == 1)
                                {
                                    answerOnStorage[0] = onOffChange[0] * startState[0];
                                    answerOnStorage[1] = onOffChange[1] * startState[1];

                                    answerOnStorage[2] = onOffChange[2] * startState[0];
                                    answerOnStorage[3] = onOffChange[3] * startState[1];

                                    alphaNumbers[0] = onHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    alphaNumbers[1] = onHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    totalNumbers[0] = alphaNumbers[0] + alphaNumbers[1];
                                    totalNumbers[1] = alphaNumbers[0] + alphaNumbers[1];

                                    answerNumbers[0] = alphaNumbers[0] / totalNumbers[0];
                                    answerNumbers[1] = alphaNumbers[1] / totalNumbers[1];

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (Math.Max(answerNumbers[0], answerNumbers[1]) == answerNumbers[0])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (Math.Max(answerNumbers[0], answerNumbers[1]) == answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                                if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerOnStorage[0] = onOffChange[0] * startState[0];
                                    answerOnStorage[1] = onOffChange[1] * startState[1];

                                    answerOnStorage[2] = onOffChange[2] * startState[0];
                                    answerOnStorage[3] = onOffChange[3] * startState[1];

                                    alphaNumbers[0] = offHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    alphaNumbers[1] = offHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    totalNumbers[0] = alphaNumbers[0] + alphaNumbers[1];
                                    totalNumbers[1] = alphaNumbers[0] + alphaNumbers[1];

                                    answerNumbers[0] = alphaNumbers[0] / totalNumbers[0];
                                    answerNumbers[1] = alphaNumbers[1] / totalNumbers[1];

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (Math.Max(answerNumbers[0], answerNumbers[1]) == answerNumbers[0])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (Math.Max(answerNumbers[0], answerNumbers[1]) == answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                            }

                            // Past the first cycle of mathematics, the calculation deviate slightly, as instead
                            // of using the initial state probability for the first section of the equation, it
                            // instead uses the answers for the previous calculation instead, also the alpha is
                            // not calculated.
                            if (j == 1)
                            {
                                if (inputOrder[j] == 0 || inputOrder[j] == 1)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = onHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = onHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                                if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = offHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = offHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                            }
                            if (j == 2)
                            {
                                if (inputOrder[j] == 0 || inputOrder[j] == 1)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = onHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = onHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                                if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = offHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = offHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                            }
                            if (j == 3)
                            {
                                if (inputOrder[j] == 0 || inputOrder[j] == 1)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = onHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = onHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                    }
                                }
                                if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerOnStorage[0] = onOffChange[0] * answerNumbers[0];
                                    answerOnStorage[1] = onOffChange[1] * answerNumbers[1];

                                    answerOnStorage[2] = onOffChange[2] * answerNumbers[0];
                                    answerOnStorage[3] = onOffChange[3] * answerNumbers[1];

                                    answerNumbers[0] = offHeat[0] * Math.Max(answerOnStorage[0], answerOnStorage[1]);
                                    answerNumbers[1] = offHeat[1] * Math.Max(answerOnStorage[2], answerOnStorage[3]);

                                    Console.WriteLine("THE PROBABILITY OF 'ON' IS {0}, THE PROBABILITY OF 'OFF' IS {1}", answerNumbers[0], answerNumbers[1]);

                                    if (answerNumbers[0] > answerNumbers[1])
                                    {
                                        outputOrder[j] = "ON";
                                        Console.WriteLine("\nThe probability of this sequence is {0}.", answerNumbers[0]);
                                    }
                                    if (answerNumbers[0] < answerNumbers[1])
                                    {
                                        outputOrder[j] = "OFF";
                                        Console.WriteLine("\nThe probability of this sequence is {0}.", answerNumbers[1]);
                                    }
                                }
                            }
                        }// end of for loop

                        Console.WriteLine("\n");

                        // This loop displays the most probable answer for each loop, by using a loop to
                        // display each result after it is pulled from an array to store them. 
                        for (k = 0; k < 4; k++)
                        {
                            Console.WriteLine("{0}\n", outputOrder[k]);
                        }
                        optionNum = 0;

                    }
                }

                // Option 3 exits the program.
                while (optionNum == 3)
                {
                    // this line of code exits the current environment.
                    Environment.Exit(0);
                }

                // Option 4 is a failsafe, which is called when the program finds an incorrect menu option.
                while (optionNum == 4)
                {
                    optionNum = 0;
                }
                // Option 5 is a failsafe, which is called when the program finds an incorrect input option. 
                while (optionNum == 5)
                {
                    optionNum = 1;
                }
            }
        }

        // This method is used to input the temperature values, so that
        // they can be used later on in the program
        static int HeatValue(int input, int[] inputLocation, int local)
        {
            // This char variable is used to stire the users input
            char valueInput;
            Console.WriteLine("\nPlease input value {0}", input);
            // Here the user inputs the value.
            valueInput = Convert.ToChar(Console.ReadLine());
            // The input is then check to see what it is, with the
            // appropriate action being taken once the character has
            // been deduced.
            if (valueInput == 'h' || valueInput == 'H')
            {
                return inputLocation[input - 1] = 0;
            }
            else if (valueInput == 'w' || valueInput == 'W')
            {
                return inputLocation[input - 1] = 1;
            }
            else if (valueInput == 'c' || valueInput == 'C')
            {
                return inputLocation[input - 1] = 2;
            }
            else if (valueInput == 'f' || valueInput == 'F')
            {
                return inputLocation[input - 1] = 3;
            }
            // If the users input is not recognised, then the program restarts the sequence.
            else
            {
                Console.WriteLine("\nThe value you entered was not an aceeptable \n\nSetting input to previous input");
                local = 5;
                return 0;
            }
        }
    }
}
