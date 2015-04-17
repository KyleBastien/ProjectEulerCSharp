using System;
using System.Collections;
using System.Collections.Generic;

namespace Problem10
{
    class Problem10
    {
        static void Main()
        {
            // The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
            //
            // Find the sum of all the primes below two million.
            //
            // Primes again? Really? Someone needs to think up something else.
            //
            // There are a couple of different ways that we could go about this problem, one of them was outlined in Problem 7 so instead of that solution, I'm going to focus on what is known
            // as the Sieve of Eratosthenes. Cool name right? That's why I picked it, and not the Sieve of Atkin.
            //
            // Sieve of Eratosthenes was as the name implies invented by Eratosthenes who was a Greek Mathematician living around 200 BC.
            //
            // The algorithm needs to have an upper limit for the primes to find. Lets call this limit N.
            //
            // The algorithm works as follow:
            // 1. Create a list l of consecutive integers {2,3,....,N}
            // 2. Select p as the first prime number in the list, p = 2.
            // 3. Remove all multiples of p from the l.
            // 4. Set p equal to the next integer in l which was not been removed.
            // 5. Repeat steps 3 and 4 until p^2 > N, all the remaining numbers in the list are primes.
            //
            // The algorithm first finds a prime, and then marks all multiples of that primes. The first new number will always be a prime, since all numbers which are not will have been removed.
            //
            // The challenge here isn't in the algorithm's implementation, but instead optimizing the algorithm, both in execution and memory wise. You can check out my optimized code in the
            // function Esieve.
            //
            // Once we have a list of all primes, the rest of the code is a trivial for loop summing up the array.

            int[] primes = ESieve(2000000);

            decimal primeSum = 0;

            for (int i = 0; i < primes.Length; i++)
            {
                primeSum += primes[i];
            }

            Console.WriteLine("Prime sum of all primes below " + 2000000 + " is " + primeSum);
            Console.ReadLine();
        }

        // This this uses a BitArray to store all the numbers. It is an enumerable type which uses one bit per boolean. Using a BitArray means the algorithm will limit the memory usage by a factor
        // of 32 compared to an array of booleans according to this: https://social.msdn.microsoft.com/Forums/vstudio/en-US/e440bd13-232d-4310-81a1-9112faa3fe32/how-do-you-work-with-bits-in-c?forum=netfxbcl
        // However, it will lower the operational performance. We need an array to hold 2,000,000 numbers, which means a difference of 250kB vs 8MB.
        //
        // After some micro testing, I didn't see much of a performance difference for a small set of primes. For large sets of primes I noticed that the BitArray was slightly faster. This is likely
        // due to better cache optimization, since the BitArray is easier to store in the CPU cache, and thus increase performance.
        //
        // I found one example of this sieve (http://digitalbush.com/2010/02/26/sieve-of-eratosthenes-in-csharp/) which skips the even numbers in the loops. I have chosen a bit of another approach,
        // to avoid allocating memory for it. It just takes a bit of keeping track of my indexes.
        //
        // Basically I want to start with three and then for a counter i = {1,2,...,N/2} represent every odd number p = {3,5,7,...,N}. That can be done as p = 2i + 1. And that is what I have done in
        // the code. It makes the code a bit more complex, but saves half the memory, and thus it can treat even larger sets.
        //
        // Furthermore we start out inner loop at p^2 = (2i + 1)(2i + 1) = 4i^2 + 4i + 1, which will have the index 2i(i+1), which is where we start the search of the inner loop. By increasing p = 2i + 1
        // indexes in every iteration of the inner loop, I am jumping 2p, and thus only taking the odd multiples of p. Since multiplying with an even number will give an even result, and therefore not
        // a prime.
        public static int[] ESieve(int upperLimit)
        {
            int sieveBound = (upperLimit - 1)/2;
            int upperSqrt = ((int) Math.Sqrt(upperLimit) - 1)/2;

            BitArray primeBits = new BitArray(sieveBound+1, true);

            for (int i = 1; i <= upperSqrt; i++)
            {
                if (primeBits.Get(i))
                {
                    for (int j = i*2*(i + 1); j <= sieveBound; j += 2*i + 1)
                    {
                        primeBits.Set(j, false);
                    }
                }
            }

            List<int> numbers = new List<int>((int)(upperLimit / (Math.Log(upperLimit) - 1.08366)));
            numbers.Add(2);

            for (int i = 1; i <= sieveBound; i++)
            {
                if (primeBits.Get(i))
                {
                    numbers.Add(2 * i + 1);
                }
            }

            return numbers.ToArray();
        }
    }
}
