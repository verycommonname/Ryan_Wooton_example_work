using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Name:     Ryan Wooton - 13394001
*  Date:     28/10/2016
 *  
 * Changes: 02/11/2016 - fixed the issue of the programme's equation
 *                       giving an impossible answer, saying that the
 *                       end possibility was over 1.
 *                       
 *          27/11/2016 - successfully implemented the true table, and all
 *                       relevent mathematics, into the program.
 *                       
 *          01/12/2016 - fixed issue of getting unfeasable
*
*  Info:    The purpose of this programme is to solve the Burglary
 *          problem given to us during lectures, using the variables
 *          givenwithin the aforementioned lecture we were tasked with
 *          finding the probability of b, given both j and m, we also
 *          had to use infernece to do this.
*/

namespace Assignment_1B
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;

            double inferenceTotal;
            double inferenceNotTotal;
            double inferenceSum;

            double eBCount = 0,
                eNotBCount = 0,
                notEBCount = 0,
                notENotBCount = 0;

            // Below, you can see the initialisation of all of the  possibility
            // variables used within the program.
            double eTrue = 0,
                   eFalse = 0,
                   pE,
                   pNotE;
            // /\ Earthquake values /\
            double bTrue = 0,
                   bFalse = 0,
                   pB,
                   pNotB;
            // /\ Burglary values /\
            double aTrue = 0,
                   aFalse = 0,
                   aBECount = 0,
                   aBNotECount = 0,
                   aNotBECount = 0,
                   aNotBNotECount = 0,
                   pAGivenBE,
                   pAGivenBNotE,
                   pAGivenNotBE,
                   pAGivenNotBNotE;
            // /\ Alarm values /\
            double jTrue = 0,
                   jFalse = 0,
                   jA = 0,
                   jNotA = 0,
                   pJA,
                   pJNotA;
            // /\ John values /\
            double mTrue = 0,
                   mFalse = 0,
                   mA = 0,
                   mNotA = 0,
                   pMA,
                   pMNotA;
            // /\ Mary values /\

            // These arrays are used by the program to compare the values for
            // each row and compare them for the necessary similarities.
            int[] earthquake = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] burglary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] alarm = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] mary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] john = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Console.WriteLine("Please input 't' for true or 'f' for false for each value.\nThe order of the values should be Earthquake, Burglary, Alarm, John, Mary.");
            
            // Since there are 10 rows in the given true table, the program loops
            // through a for loop 10 times, with each loop representing a row on the
            // true table.
            for (int i=0;i<10;i++)
            {
                // Here the user inputs the values for each row, the length of the string
                // is irrelevent, as it stops checking after the first 5 characters
                // using the second for loop below
                userInput = Console.ReadLine();

                for(int j=0;j< userInput.Length;j++)
                {
                    if (j == 0)
                    {
                        // for the first character within the string, the program checks whether
                        // the user has inputting 't' for true or 'f' for false, if it is true
                        // then the program will mark the appropriate comparison array location as a true
                        // value, in this case a 1, and add 1 to a counter that tracks how many
                        // true values for that variable.
                        // The inverse happens when the program detects a false value, or an incorrect
                        // value, the appropriate array will be marked with a 0, which indicates false,
                        // and 1 is added to a counter that tracks the number of falses that variable
                        // gets, this process is then repeated for all of the other variables and occurs
                        // for every row in the table
                        if(userInput[j] == 't' || userInput[j] == 'T')
                        {
                            earthquake[i] = 1;
                            eTrue++;
                        }
                        if (userInput[j] == 'f' || userInput[j] == 'F')
                        {
                            earthquake[i] = 0;
                            eFalse++;
                        }
                    }
                    if (j == 1)
                    {
                        if (userInput[j] == 't' || userInput[j] == 'T')
                        {
                            burglary[i] = 1;
                            bTrue++;
                        }
                        if (userInput[j] == 'f' || userInput[j] == 'F')
                        {
                            burglary[i] = 0;
                            bFalse++;
                        }
                    }
                    if (j == 2)
                    {
                        if (userInput[j] == 't' || userInput[j] == 'T')
                        {
                            alarm[i] = 1;
                            aTrue++;
                        }
                        if (userInput[j] == 'f' || userInput[j] == 'F')
                        {
                            alarm[i] = 0;
                            aFalse++;
                        }
                    }
                    if (j == 3)
                    {
                        if (userInput[j] == 't' || userInput[j] == 'T')
                        {
                            john[i] = 1;
                            jTrue++;
                        }
                        if (userInput[j] == 'f' || userInput[j] == 'F')
                        {
                            john[i] = 0;
                            jFalse++;
                        }
                    }
                    if (j == 4)
                    {
                        if (userInput[j] == 't' || userInput[j] == 'T')
                        {
                            mary[i] = 1;
                            mTrue++;
                        }
                        if (userInput[j] == 'f' || userInput[j] == 'F')
                        {
                            mary[i] = 0;
                            mFalse++;
                        }
                    }
                    else
                    {
                        // Do nothing
                    }
                } // end of foreach loop

                // Below you can see the program comparing the values within the arrays
                // to check for any similarities in each column, if it detects any
                // similaries then the program will add a 1 to a counter that tracks
                // how many times said similarity has occurred.
                if(earthquake[i] == 1 && burglary[i] == 1 && alarm[i] == 1)
                {
                    aBECount++;
                }
                if (earthquake[i] == 0 && burglary[i] == 1 && alarm[i] == 1)
                {
                    aBNotECount++;
                }
                if (earthquake[i] == 1 && burglary[i] == 0 && alarm[i] == 1)
                {
                    aNotBECount++;
                }
                if (earthquake[i] == 0 && burglary[i] == 0 && alarm[i] == 1)
                {
                    aNotBNotECount++;
                }
                
                if (earthquake[i] == 1 && burglary[i] == 1)
                {
                    eBCount++;
                }
                if (earthquake[i] == 0 && burglary[i] == 1)
                {
                    notEBCount++;
                }
                if (earthquake[i] == 1 && burglary[i] == 0)
                {
                    eNotBCount++;
                }
                if (earthquake[i] == 0 && burglary[i] == 0)
                {
                    notENotBCount++;
                }

                if (john[i] == 1 && alarm[i] == 1)
                {
                    jA++;
                }
                if (john[i] == 1 && alarm[i] == 0)
                {
                    jNotA++;
                }
                if (mary[i] == 1 && alarm[i] == 1)
                {
                    mA++;
                }
                if (mary[i] == 1 && alarm[i] == 0)
                {
                    mNotA++;
                }
            } // end of for loop
            
            // In order to  compute the total probability through inference, we must first
            // derive the estimation of the parameters.

            // To derive the probability of earthquake and burglary being both true and false,
            // the program first takes the number of times the wanted value has occured plus
            // 1, and divides it by the total number of values for that variable plus the total
            // number of available states, in all of the given cases, this latter values equates
            // to 10, the total number of starts for a variable, plus 2 as the variable can either
            // be true or false.
            pE = (eTrue + 1)/12d;
            pNotE = (eFalse + 1) / 12d;

            pB = (bTrue + 1d) / 12d;
            pNotB = (bFalse + 1d) / 12d;

            // To derive the probability of the alarm is a more complicated matter then for
            // earthquake and burglary, but follows most of the same steps, the first step is
            // the same as before, taking a count of how many times the desired probability occures
            // and adding one, however this is then divided by the sum of the total number of possible
            // outcomes, which in this case is 2, and the count of both of the dependences, derived
            // by having an if statement count up when it detects the both values are correct for the line.
            pAGivenBE = ((aBECount + 1d) / (eBCount + 2d));
            pAGivenBNotE = ((aBNotECount + 1d) / (notEBCount + 2d));
            pAGivenNotBE = ((aNotBECount + 1d) / (eNotBCount + 2d));
            pAGivenNotBNotE = ((aNotBNotECount + 1d) / (notENotBCount + 2d));

            // The final set of probabilities to estimate use the same calculation as the previous set,
            // the alarm set, however the calculation is now simpler due to only having one dependency
            pJA = ((jA + 1d)/(aTrue + 2d));
            pJNotA = ((jNotA + 1d) / (aFalse + 2d));

            pMA = ((mA + 1d) / (aTrue + 2d));
            pMNotA = ((mNotA + 1d) / (aFalse + 2d));

            // Finally, the total probability of P(b|j, m) is calculated using inference via enumeration by 
            // first computing all the probabilities given that b is true and summing them all together, then all
            // of the probabilities given that b is not true are calculated and added together, the sum of the
            // not b values are then added to the sum of the b values to give the alpha.
            inferenceTotal = ((((pAGivenBE * pJA * pMA) + ((1 - pAGivenBE) * pJNotA * pMNotA)) * pE)
                +(((pAGivenBNotE * pJA * pMA) + ((1 - pAGivenBNotE) * pJNotA * pMNotA)) * pNotE)) * pB;

            inferenceNotTotal = ((((pAGivenNotBE * pJA * pMA) + ((1 - pAGivenNotBE) * pJNotA * pMNotA)) * pE)
                + (((pAGivenNotBNotE * pJA * pMA) + ((1 - pAGivenNotBNotE) * pJNotA * pMNotA)) * pNotE)) *pNotB;
            
            inferenceSum = inferenceTotal + inferenceNotTotal;

            // The total sum of b is then divided by the alpha in order to get the final answer.
            inferenceSum = inferenceTotal / inferenceSum;

            // The program then prints the answer.
            Console.WriteLine("{0} is the possibility of (b|j, m).", inferenceSum);

            Console.ReadLine();
        }
    }
}

