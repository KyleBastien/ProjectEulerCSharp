using System;
using System.Collections.Generic;
namespace Problem21
{
    class Problem21
    {
        static void Main()
        {
            // Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
            // If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
            //
            // For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
            //
            // Evaluate the sum of all the amicable numbers under 10000.
            //
            // We've discussed in previous solutions that we can find the divisors of a number can be done very efficiently using prime factorisation. We're going to deduce the sum of divisors based on a 
            // prime facotrisation of a number.
            //
            // In the problem d(n) is defined as the sum of proper divisors. However, we will be finding a method for deducing all divisors, so lets call that function t(n) = d(n) + n. So as you can see
            // there is a simple relationship between them.
            //
            // In general this function can be described as :
            //
            // t(n) = summation from d|n of d
            //
            // Where the notation d|n means the divisors d of the number n.
            //
            // For any prime p, this function would be t(p) = 1 + p, since primes only have the factors one and the number itself.
            //
            // If we now consider p^a, where a is an integer than we have...
            //
            // t(p^a) = 1 + p + p^2 + p^3 + ... + p^a
            //
            // and if we multiply by p we have...
            //
            // p * t(p^a) = p + p^2 + p^3 + ... + p^(a+1)
            //
            // we can subtract these two formulas for each other and get...
            //
            // p * t(p^a) - t(p^a) = (p - 1) * t(p^a) = p^(a+1) - 1
            //
            // Which can be rearranged to...
            //
            // t(p^a) = p^(a+1) - 1 / p - 1
            //
            // So far we have deduced a function which works for a prime factorisation consisting of one prime, or a multiple of that prime. In order to generalise the function to any number, we will need to
            // show that the function is multiplicative for coprimes. Since if it is multiplicative for coprimes, then it will work for any prime factorisation, since any number and a prime are coprimes. None
            // of the aforementioned sources provide a proof of the multiplicative property, but that shouldn't stop us from deducing it.
            //
            // Let both m,n be positive integers and coprime. Assuming that the function is multiplicative we have that:
            //
            // t(mn) = summation from d|nm of d
            //
            // The Fundamental Theorem of Arithmetic states that each divisors of mn is a product of two unique positive coprimes integers d1 and d2 dividing m and n. Thus we can write...
            //
            // t(mn) = summation from d|nm of d = summation from d1|n of the summation from d2|m of d1d2 = (summation from d1|n of d1)(summation from d2|m of d2) = t(n)t(m)
            //
            // The second to last rearrangement follows from the multiplicativeness of scalars.
            //
            // Hence for coprimes the function is multiplicative, so we can expand the function from a prime to any number:
            //
            // t(n) = the product from p|n of p^(a+1) - 1 / p - 1
            //
            // where the product runs over the set of distinct prime factors of n. Upon some more research it turns out that Wolfram gives us another source for divisor functions as it refers to Berndt
            // 1985 (http://www.amazon.com/exec/obidos/ASIN/0387961100/mathblogdk-20) as a source of this function. In order words we can find the sum of factors for an arbitary number, if we have the prime
            // factorisation.
            // 
            // If a prime has the exponent 1 then the formula becomes:
            //
            // d(p^a) = p^2 - 1 / p - 1 = (p + 1)(p - 1) / (p - 1) = p + 1
            //
            // Something which we will see once we implement the function. In the solution to problem 3 we made a prime factorisation for the first time. We used a similar function to answer problem 5 as well.
            // Lets modify both solutions to generate the sum of factors instead. Look to SumOfFactorsPrime.
            //
            // When we use the SumOfFactorsPrime we will need a list of primes, which we can easily generate with the Sieve of Eratosthenes which we constructed in Problem 10.
            //
            // All of this gets us about 5x faster of a function as compared to a Brute Force solution. But WE CAN DO BETTER!
            //
            // The last thing we can do in order to save operations is to store the results. If the divisor sum is larger than the number itself, we can store it for later instead of checking that larger number.
            // That also means we have to check the storage if the sum of factors is less than the number.
            //
            // I have chosen a dictionary, since that is ideal when you have a key and a data object, and we have exactly that. The key is the sum of divisors, adn the object is the number itself.

            const int upperLimit = 10000;
            int sumAmicible = 0;
            int[] primelist = Problem10.Problem10.ESieve((int)Math.Sqrt(upperLimit) + 1);
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 2; i <= upperLimit; i++)
            {
                int factors = SumOfFactorsPrime(i, primelist);
                if (factors > i)
                {
                    dic.Add(i, factors);
                } 
                else if (factors < i)
                {
                    if (dic.ContainsKey(factors))
                    {
                        if (dic[factors] == i)
                        {
                            sumAmicible = sumAmicible + i + factors;
                        }
                    }    
                }
            }

            // The time saved after adding caching is basically negligible, I maybe saved 1ms. So it might not be worth but I enjoy *highly* optimized solutions.

            Console.WriteLine("The largest sum of all amicable pairs less than "+upperLimit+": "+sumAmicible);
            Console.ReadLine();
        }

        public static int SumOfFactorsPrime(int number, int[] primelist)
        {
            int n = number;
            int sum = 1;
            int p = primelist[0];
            int i = 0;

            while (p*p <= n && n > 1 && i < primelist.Length)
            {
                p = primelist[i];
                i++;
                if (n%p == 0)
                {
                    int j = p*p;
                    n = n/p;
                    while (n%p == 0)
                    {
                        j = j*p;
                        n = n/p;
                    }
                    sum = sum*(j - 1)/(p - 1);
                }
            }

            // A prime factor larger than the square root remains
            if (n > 1)
            {
                sum *= n + 1;
            }
            return sum - number;
        }
    }
}
