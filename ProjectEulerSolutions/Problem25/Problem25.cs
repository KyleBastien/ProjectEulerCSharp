using System;
using System.Numerics;
namespace Problem25
{
    class Problem25
    {
        static void Main()
        {
            // The Fibonacci sequence is defined by the recurrence relation:
            //
            // Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
            // Hence the first 12 terms will be:
            //
            // F1 = 1
            // F2 = 1
            // F3 = 2
            // F4 = 3
            // F5 = 5
            // F6 = 8
            // F7 = 13
            // F8 = 21
            // F9 = 34
            // F10 = 55
            // F11 = 89
            // F12 = 144
            // The 12th term, F12, is the first term to contain three digits.
            //
            // What is the first term in the Fibonacci sequence to contain 1000 digits?
            //
            // So *technically* there is a really good solution using the constant equation to calculate a number in the Fibonacci sequence. That being said that equation requires you to be able to store
            // the number Phi, which is an irrational number. I'm not sure if there is a good way of doing this in C# though, so I'm not going to use this solution and instead show the Brute Force solution.
            // Feel free though to submit a pull request with the other solution.
            //
            // Another thing about the Brute Force solution is that the BigInteger class makes it *very* easy.

            int i = 0;
            int cnt = 2;
            BigInteger limit = BigInteger.Pow(10, 999);
            BigInteger[] fib = new BigInteger[3];

            fib[0] = 1;
            fib[2] = 1;

            while (fib[i] <= limit)
            {
                i = (i + 1)%3;
                cnt++;
                fib[i] = fib[(i + 1)%3] + fib[(i + 2)%3];
            }

            Console.WriteLine("The first term in the fibonnaci sequence to have more than 1000 digits is term number: "+cnt);
            Console.ReadLine();
        }
    }
}
