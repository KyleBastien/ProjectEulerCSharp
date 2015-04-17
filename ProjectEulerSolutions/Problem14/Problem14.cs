using System;
namespace Problem14
{
    class Problem14
    {
        static void Main()
        {
            // The following iterative sequence is defined for the set of positive integers:
            //
            // n → n/2 (n is even)
            // n → 3n + 1 (n is odd)
            //
            // Using the rule above and starting with 13, we generate the following sequence:
            //
            // 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
            // It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
            //
            // Which starting number, under one million, produces the longest chain?
            //
            // NOTE: Once the chain starts the terms are allowed to go above one million.
            //
            // The direct approach to something like this is very simple, all you need to do is loop over all the numbers, run the sequence until it converges, and count the length. Nothing really fancy but that
            // solution is very slow, so instead I'm going to go for a caching solution to save some time.
            //
            // There is nothing too mysterious over the caching method either. We just store the results we have already calculated. We use an array as cache, since I know the necessary and sufficient cache size
            // (needs to store 1,000,000 resutls) ahead of execution. At the cost approximately a 1MB of RAM.
            //
            // Since we start from the smallest number, I know that every time my sequence of numbers decreases below the starting number, I have already calculated the reamining starting length. So for even numbers
            // that happens after the first iteration.
            //
            // For the example given in the problem description:
            // 13 -> 40 -> 20 -> 10 -> 5 -> 16 -> 8 -> 4 -> 2 -> 1
            //
            // In the third iteration we hit the number 10, which is smaller than 13, and thus I have already calculated the sequence length for the number 10, so I can just add the length of the sequence I have
            // already went through to get the total result.

            const int number = 1000000;

            int sequenceLength = 0;
            int startingNumber = 0;
            long sequence = 0;

            int[] cache = new int[number+1];
            // Initialise cache
            for (int i = 0; i < cache.Length; i++)
            {
                cache[i] = -1;
            }
            cache[1] = 1;

            for (int i = 2; i <= number; i++)
            {
                sequence = i;
                int k = 0;
                while (sequence != 1 && sequence >= i)
                {
                    k++;
                    if ((sequence%2) == 0)
                    {
                        sequence = sequence/2;
                    }
                    else
                    {
                        sequence = sequence*3 + 1;
                    }
                }
                // Store
                cache[i] = k + cache[sequence];

                // Check if sequence is the best solution
                if (cache[i] > sequenceLength)
                {
                    sequenceLength = cache[i];
                    startingNumber = i;
                }
            }

            Console.WriteLine("The starting number "+startingNumber+" produces a sequence of "+sequenceLength);
            Console.ReadLine();
        }
    }
}
