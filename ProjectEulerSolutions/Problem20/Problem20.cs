using System;
using System.Numerics;
namespace Problem20
{
    class Problem20
    {
        static void Main()
        {
            // n! means n × (n − 1) × ... × 3 × 2 × 1
            //
            // For example, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800,
            // and the sum of the digits in the number 10! is 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27.
            //
            // Find the sum of the digits in the number 100!
            //
            // 100! is a VERY large number, but similar to Problem 13 or Problem 16 this is trivialized by the fact that we have access to the BigNumber class since .NET 4.0.
            //
            // This solution is actually very similar to Problem 16.

            int sum = 0;
            BigInteger fac = 1;

            for (int i = 2; i <= 100; i++)
            {
                fac *= i;
            }

            while (fac > 0)
            {
                sum += (int) (fac%10);
                fac /= 10;
            }

            Console.WriteLine("The sum of digits in 100! is: "+sum);
            Console.ReadLine();
        }
    }
}
