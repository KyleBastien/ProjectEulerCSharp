using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5
{
    class Problem5
    {
        static void Main()
        {
            // 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
            //
            // What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
            //
            // In Problem 3 we discussed prime factorisation and we can deduct two important properties we cna use for this problem:
            // 1) All numbers have a unique prime factorisation
            // 2) All non-prime factors of a number, can be generated from the prime factors
            //
            // Using this we can restate the problem as:
            // Find the smallest set of primes, such that all numbers 1-20 can be constructed. This set will be the prime factorisation of the smallest
            // number to which 1-20 are all evenly divisble.
            //
            // We denote the biggest factor we want to investigate as k, such that we want to find the solution for k=20. Furthermore we define the solution as N.
            // Let start by looking at the first few numbers of k:
            //
            // k = 2, N = 2
            // k = 3, N = 2 * 3 = 6
            // k = 4, N = 2 * 3 * 2 = 12
            //
            // The prime factorisation of 4 is 2 * 2, but we already have one of the factors we need form the factorisation of 2. Therefore we only need to add one 2,
            // to find a set of prime factors so we cna describe all number 1-4. This goes on until we reach k = 20.
            //
            // How do we make this into an algorithm though?
            //
            // Let p-i be the ith prime, then N can be expressed as N = p1 ^ a1 * p2 ^ a2 * p3 ^ a3...
            //
            // We start by putting a-i = 0 for all i. We can iterate over all the divisors, such that divisor j = 1,2,...,20 can be factorised as N-j = p1 ^ b1 * p2 ^ b2 * p3 ^ b3...
            // and then a-i = max(a-i, b-i).
            //
            // We can factorise each j=1,2,...20 as we did in Problem 3, or we can investigate the size of a-i a bit more. The exponent a-i is the largest natural number which result
            // in p-i ^ a-i <= k.
            //
            // For p-i = 2, we have that 2^4 = 16 and 2^5 = 32. Therefore a1 = 4. In more general terms we can express a-i = floor(log(k) / log(p-i)).
            //
            // So under the assumption that we have the primes, we can rather easily find the exponents, needed to express N. First thing we need to find is a list of primes, which can
            // be found in the function GeneratePrimes.
            //
            // Now that we have a method for finding our primes, we can also make the algorithm which can be written as:

            int divisorMax = 20;
            int[] p = GeneratePrimes(divisorMax);
            int result = 1;

            for (int i = 0; i < p.Length; i++)
            {
                int a = (int) Math.Floor(Math.Log(divisorMax)/Math.Log(p[i]));
                result = result*((int) Math.Pow(p[i], a));
            }

            Console.WriteLine("The smallest number dividable with all number 1-20 is "+result);
            Console.ReadLine();
        }

        // We can check all odd numbers, and see if they are divisible with an already found prime, until we reach the square root of the number. If no prime divisor is found, then the
        // number is also a prime. It is a pretty efficient algorithm which builds on many of the principles we are already using.
        private static int[] GeneratePrimes(int upperLimit)
        {
            List<int> primes = new List<int>();
            bool isPrime;
            int j;

            primes.Add(2);

            for (int i = 3; i <= upperLimit; i += 2)
            {
                j = 0;
                isPrime = true;
                while (primes[j]*primes[j] <= i)
                {
                    if (i%primes[j] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    j++;
                }
                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            return primes.ToArray<int>();
        }
    }
}
