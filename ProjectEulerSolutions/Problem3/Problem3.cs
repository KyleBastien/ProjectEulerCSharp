using System;

namespace Problem3
{
    class Problem3
    {
        static void Main()
        {
            // The prime factors of 13195 are 5, 7, 13 and 29.
            //
            // What is the largest prime factor of the number 600851475143 ?
            // 
            // The non-brute forcing solution to this problem surround the *Fundamental Theorem of Arithmetic* which states:
            // Any integer greater than 1 is either a prime number, or can be written as a unique product of prime numbers (ignoring the order).
            // There is a full breakdown of this theorem at the link below if you want to check it out:
            // http://www.mathsisfun.com/prime-factorization.html
            //
            // Basically instead of finding all prime numbers first we are "just" going to enumerate through all numbers until we have a complete 
            // factorization. All non-prime factors will already be factorized in primes, as per the theorem. It's probably easier to explain in code.

            const long num = 600851475143;
            long tempnum = num;
            long largestFact = 0;

            int counter = 2;
            while (counter*counter <= tempnum)
            {
                if (tempnum%counter == 0)
                {
                    tempnum = tempnum/counter;
                }
                else
                {
                    counter = (counter == 2) ? 3 : counter + 2;
                }
            }

            if (tempnum > largestFact) // the remainder = prime number
            {
                largestFact = tempnum;
            }

            // What I'm doing is starting with 2 and check how many times that number is divisible with the remainder we have. Once 2 is not a divisor to the remainder
            // we increase the counter by one and check 3, 4 and so on. If 4 was a factor to the original number, 2 will be a factor at least twice as well. And then we
            // can go on, until the counter is larger than the square root of the remainder.
            //
            // If the remainder is different from one, then the remainder is also a prime factor. However we only need to check if it is larger than the largest factor found,
            // to see if it is interesting.
            //
            // Technically, I could replace the complex counter = ... assignment above with counter++ but I only need to increase by one in the first loops, when counter = 2. 
            // Otherwise, I can increase counter by 2 and save some iterations.

            Console.WriteLine("Largest Prime Factor of 600851475143 is: "+largestFact);
            Console.ReadLine();
        }
    }
}
