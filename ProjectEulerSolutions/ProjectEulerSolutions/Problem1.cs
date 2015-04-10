using System;

namespace ProjectEulerSolutions
{
    class Problem1
    {
        static void Main()
        {
            // Multiples of 3 and 5
            // If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
            // Find the sum of all the multiples of 3 or 5 below 1000.

            // I'm going to focus on the constant [O(1)] solution here using a geometric approach.
            // I will write a function to find the sum of all numbers divisible by three, and the sum of all numbers divisible by 5. 
            // This is the SumDivisbleBy function below.

            // This solution counts the numbers which are divisible by 3 and 5 twice, and therefore we need to subtract them, hence....
            int result = SumDivisbleBy(3, 999) + SumDivisbleBy(5, 999) - SumDivisbleBy(15, 999);
            Console.WriteLine("The sum of all numbers dividable by 3 or 5 is: " + result);
            Console.ReadLine();
        }

        // Here n is the divisor, and p is the greatest number we want to check. This is defined as 1000-1 (999) by our question, and n is the set
        // 3 and 5. I'm going to use arithmetic progression, or a geometric series, to reduce the sum:
        //
        //                                                      1+2+3+4+..+N to (N * (N + 1) / 2).
        //
        // Thus the sequences for any number divisible by n can be written as.... n * N * (N + 1) / 2
        // N is the highest number less than p divisble by n. In the case of 5 this is 995. But can also be expressed as N = p / n
        private static int SumDivisbleBy(int n, int p)
        {
            return n * (p / n) * ((p / n) + 1) / 2;
        }
    }
}
