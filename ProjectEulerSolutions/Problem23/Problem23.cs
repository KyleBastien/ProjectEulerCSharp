using System;
using System.Collections.Generic;
namespace Problem23
{
    class Problem23
    {
        static void Main()
        {
            // A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means 
            // that 28 is a perfect number.
            //
            // A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.
            //
            // As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all 
            // integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that 
            // cannot be expressed as the sum of two abundant numbers is less than this limit.
            //
            // Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
            //
            // So my approach can be broken down to three major steps...
            //
            // 1. Find all abundant numbers
            // 2. Create and mark all number which can be created as the sum of two abundant numbers
            // 3. Sum up all non-marked numbers
            //
            // In Problem 21 we spent a lot of time on developing an algorithm that can effectively calculate the sum of factors. So I will reuse that function, and from there it becomes trivial to calculate all the
            // abundant numbers. See FindAbundentNumbers for details.

            const int limit = 28123;
            List<int> abundent = FindAbundentNumbers(limit);

            // Now we need to create all sums of numbers.
            //
            // I've done this by creating two nested for loops, where the outer runs through all the found abundant numbers, and the inner loops runs through all abundant numbers equal to or larger than the current
            // position of the outer loop. The actual implementation is in the MakeAllSumsOfTwoAbundentNumbers function.

            bool[] canBeWrittenAsAbundent = MakeAllSumsOfTwoAbundentNumbers(limit, abundent);

            // Now we have a list of all the numbers below our limit that can and cannot be written as a sum of two abundant numbers. So now the only thing left is to sum it up.
            long sum = 0;
            for (int i = 1; i <= limit; i++)
            {
                if (!canBeWrittenAsAbundent[i])
                {
                    sum += i;
                }
            }

            Console.WriteLine("The sum of all numbers that cannot be written as the sum of two abundant numbers is "+sum);
            Console.ReadLine();

            // This solution though would be what I would count as a Brute Force solution to this problem, not a clever solution. I would really like to revist this Problem at some point to see if I can make it
            // any better.
        }

        // One little trick, we already know that all numbers above 28123 can be written as a sum of two abundant numbers, and we also know that the list is sorted ascendantly. So as soon as we reach a sum which
        // is above 28123, we can break out of the inner loop.
        //
        // For marking purposes I've chosen a boolean array to mark the numbers which can be written as abundant numbers.
        public static bool[] MakeAllSumsOfTwoAbundentNumbers(int limit, List<int> abundent)
        {
            bool[] canBeWrittenAsAbundent = new bool[limit + 1];
            for (int i = 0; i < abundent.Count; i++)
            {
                for (int j = i; j < abundent.Count; j++)
                {
                    if (abundent[i] + abundent[j] <= limit)
                    {
                        canBeWrittenAsAbundent[abundent[i] + abundent[j]] = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return canBeWrittenAsAbundent;
        }

        public static List<int> FindAbundentNumbers(int limit)
        {
            List<int> abundent = new List<int>();
            int[] primelist = Problem10.Problem10.ESieve((int) Math.Sqrt(limit));

            // Find all abundant numbers
            for (int i = 2; i <= limit; i++)
            {
                if (Problem21.Problem21.SumOfFactorsPrime(i, primelist) > i)
                {
                    abundent.Add(i);
                }
            }

            return abundent;
        } 
    }
}
