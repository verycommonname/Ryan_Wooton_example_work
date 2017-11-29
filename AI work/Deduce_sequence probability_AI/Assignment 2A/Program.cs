using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name:     Ryan Wooton - 13394001
*  Date:     28/10/2016
 *  
 * Changes: 23/11/2016 - Program complete
 * 
 *          01/12/2016 - implemented failsafe for option 1.
*
*  Info:    The purpose of this programme is to compute the probability
*           of observing a pattern of emission from a Hidden Markov Model
*           given a number of possibilities for element to change using the
*           bracketed means of calculation.
*/
namespace Assignment_2A
{
    class Program
    {
        static void Main(string[] args)
        {
            // Below we can see all of the variables being initialised
            int optionNum = 0,
                i,
                j;
            double finalSumTotal = 0;
            bool programmeRun = true;
            string userInput;
            double[] startState = { 0.5, 0.5 };
            // [0] to stay the same, [1] to change.
            double[] onOffChange = { 0.75, 0.25, 0.25, 0.75 };
            //double[] OffToOn = { 0.75, 0.25 };

            // Sequence goes hot, warm, clod, freezing
            double[] onHeat = { 0.45, 0, 0, 0.05 };
            double[] offHeat = { 0.05, 0, 0, 0.45 };
            double[] answerStorage = { 0, 0 };
            double[] answerNumbers = { 0, 0 };
            int[] inputOrder = { 0, 0, 0, 0 };

            // this ensures that the program will always run until the program is exited.
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
                            // begin a recursive algorithm that makes use of multiple matrices, with the only
                            // diference between the two for each step being the use of the values for when
                            // it is on for 'hot' and 'warm' and the use of the off values for when it is 'cold'
                            // or 'freezing'.
                            if (j == 0)
                            {
                                if (inputOrder[j] == 0 || inputOrder[j] == 1)
                                {
                                    answerStorage[0] = ((onOffChange[0] * startState[0]) + (onOffChange[1] * startState[1]));
                                    answerStorage[1] = ((onOffChange[2] * startState[0]) + (onOffChange[3] * startState[1]));

                                    answerNumbers[0] = ((onHeat[0] * answerStorage[0]) + (onHeat[1] * answerStorage[1]));
                                    answerNumbers[1] = ((onHeat[2] * answerStorage[0]) + (onHeat[3] * answerStorage[1]));

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
                                }
                                else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerStorage[0] = ((onOffChange[0] * startState[0]) + (onOffChange[1] * startState[1]));
                                    answerStorage[1] = ((onOffChange[2] * startState[0]) + (onOffChange[3] * startState[1]));

                                    answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                    answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
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

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
                                }
                                else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                    answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                    answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                    answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
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

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
                                }
                                else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                    answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                    answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                    answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
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

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
                                }
                                else if (inputOrder[j] == 2 || inputOrder[j] == 3)
                                {
                                    answerStorage[0] = ((onOffChange[0] * answerNumbers[0]) + (onOffChange[1] * answerNumbers[1]));
                                    answerStorage[1] = ((onOffChange[2] * answerNumbers[0]) + (onOffChange[3] * answerNumbers[1]));

                                    answerNumbers[0] = ((offHeat[0] * answerStorage[0]) + (offHeat[1] * answerStorage[1]));
                                    answerNumbers[1] = ((offHeat[2] * answerStorage[0]) + (offHeat[3] * answerStorage[1]));

                                    Console.WriteLine("{0}  {1}", answerNumbers[0], answerNumbers[1]);
                                }
                            }

                        }// end of for loop

                        // Once the program has successfully got the two possible answers, they are added
                        // together to create the final sum for the inputted sequence.
                        finalSumTotal = answerNumbers[0] + answerNumbers[1];
                        Console.WriteLine("The final result is {0}", finalSumTotal);
                        optionNum = 0;

                    }
                }

                // Option 3 exits the program
                while (optionNum == 3)
                {
                    // this line of code exits the current environment.
                    Environment.Exit(1);
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

        static int HeatValue(int input, int[] inputLocation, int local)
        {
            char valueInput;
            Console.WriteLine("\nPlease input value {0}", input);
            // When called, this method will ask the user to input a single value, once they've entered
            // the value the program will check to see what they've entered and change the appropriate value
            // of the inputOrder array to match what was inputted.
            valueInput = Convert.ToChar(Console.ReadLine());
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
            // If the users input is not recognised, then the program restarts the sequence
            else
            {
                Console.WriteLine("\nThe value you entered was not an aceeptable \n\nRestarting sequence");
                local = 5;
                return 0;
            }
        }
    }
}
