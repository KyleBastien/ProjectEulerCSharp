using System;
using System.Numerics;
namespace Problem16
{
    class Problem16
    {
        static void Main()
        {
            // 215 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
            //
            // What is the sum of the digits of the number 2^1000?
            //
            // This is another problem where if BigInteger didn't exit you would need to really think about this one. But thanks to BigInteger this is trival....Yay C#...

            int result = 0;

            BigInteger number = BigInteger.Pow(2, 1000);

            while (number > 0)
            {
                result += (int) (number%10);
                number /= 10;
            }

            // Here I just used the modulos operator to slice the last digit off and add it to the result. Then the divide operator to remove the last digit from the number so we don't add it again.
            
            Console.WriteLine("The sum of digits of 2^1000 is: "+result);
            Console.ReadLine();
        }
    }
}
