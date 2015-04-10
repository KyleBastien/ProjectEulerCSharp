using System;
using System.Collections.Generic;

namespace Problem7
{
    class Problem7
    {
        static void Main()
        {
            // By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
            //
            // What is the 10,001st prime number?
            //
            // Are Prime Numbers a favorite of Computer Science questions or what?
            //
            // One way to solve this is to adapt the method in Problem 5 such that we only need to divide by known primes, in order to check if the new number is a prime. It is still using trial
            // division, but we can limit the number of operations needed to check if a number is a prime.
            // 
            // The downside of this approach is that we have to store all the found primes, which in this case means storing 10,001 prime numbers. But the code can be written as:

            int upperLimit = 10001;
            int counter = 1;
            List<int> primes = new List<int> {2};

            while (primes.Count < upperLimit)
            {
                counter += 2;
                int j = 0;
                bool isPrime = true;
                while (primes[j]*primes[j] <= counter)
                {
                    if (counter%primes[j] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    j++;
                }
                if (isPrime)
                {
                    primes.Add(counter);
                }
            }

            Console.WriteLine("The "+upperLimit+"st Prime Number is: "+counter);
            Console.ReadLine();

            // This algorithm actually ends up using more memory than a brute force solution would, and there is very little difference in actual execution time. However if you increase the number
            // of primes you want to find, there is a growing difference.
        }
    }
}
