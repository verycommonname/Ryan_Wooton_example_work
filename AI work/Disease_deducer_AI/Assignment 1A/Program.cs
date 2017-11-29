using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name:     Ryan Wooton - 13394001
*  Date:     28/10/2016
 *  
 * Changes: 20/11/2016 - Completed program
*
*  Info:    The purpose of this program is to use the law of probability
*           and bayes theorem in order to calculate the probability of
*           disease being true given the test is also true.
*/
namespace Assignment_1A
{
    class Program
    {
        static void Main(string[] args)
        {
            // Here we see the initiation of the variables used within the programme
            // which includes the true and false values for D and T values and a
            // bool value to ensure the programme continues to loop.
            int optionNum = 0;
            string userInput;
            bool programmeRun = true;
            float pDVal = 0,
                pTVal = 0,
                pNotDVal = 0,
                pNotTVal = 0,
                pTAndDVal = 0,
                pNotTAndNotD = 0,
                finalSumTotal = 0;

            // This while loop serves the purpose of ensuring the programme continues
            // to loop through if the user wishs to re-enter their values.
            while (programmeRun == true)
            {
                // Option one provides the user with a number of options to choose from.
                while (optionNum == 0)
                {
                    Console.WriteLine("Please press (1) to input your variables, (2) to view P(d│t) results, press (3) to exit program");
                    userInput = Console.ReadLine();
                    /* Here we see a basic form or error handling in action, first the programme
                     * checks to see if the values(s) the user inputted, if the values are not 1,
                     * 2 or 3, if the input is neither of these, then the programme triggers a
                     * hidden fifth option that resets the option, if it is one of these options,
                     * then that option is triggered
                     */
                    if (userInput != "1" & userInput != "2" & userInput != "3")
                    {
                        Console.WriteLine("\nThe value you entered was not an aceeptable \n");
                        optionNum = 4;
                    }
                    else
                    {
                        optionNum = Convert.ToInt16(userInput);
                    }
                }

                while (optionNum == 1)
                {
                    Console.WriteLine("Please input value for P(d)");
                    pDVal = Convert.ToSingle(Console.ReadLine());
                    Console.WriteLine("Please input value for P(t│d)");
                    pTAndDVal = Convert.ToSingle(Console.ReadLine());
                    Console.WriteLine("Please input value for P(¬t│¬d)");
                    pNotTAndNotD = Convert.ToSingle(Console.ReadLine());
                    /* Above is where the aforementioned values are inputed into the programme and stored
                     * in the appropriate location, below the values of ¬d, t, ¬t and the final sum total
                     * are calculated before returning to the original option screen.
                     */
                    pNotDVal = (1 - pDVal);
                    pTVal = ((1 - pNotTAndNotD) * pNotDVal) + (pTAndDVal * pDVal);
                    pNotTVal = (1 - pTVal);
                    finalSumTotal = (pTAndDVal * pDVal) / pTVal;
                    optionNum = 0;
                }

                // This option diplays the currently calculated result, if one is available, if not then
                // the programme shows an error warning and returns to the main option using the same 
                // method as in option 0.
                while (optionNum == 2)
                {
                    if (finalSumTotal == 0)
                    {
                        Console.WriteLine("\nThere is no total to show\n");
                        optionNum = 4;
                    }
                    else
                    {
                        Console.WriteLine("The result for P(d|t) is {0}", finalSumTotal);
                        optionNum = 0;
                    }
                }

                // This option exits the program when triggered
                while (optionNum == 3)
                {
                    System.Environment.Exit(1);
                }

                // This option is triggered when the first option throws an error
                while (optionNum == 4)
                {
                    optionNum = 0;
                }
            }
        }
    }
}
