using System;

namespace Problem6
{
    class Problem6
    {
        static void Main()
        {
            // The sum of the squares of the first ten natural numbers is,
            //
            // 1^2 + 2^2 + ... + 10^2 = 385
            // The square of the sum of the first ten natural numbers is,
            //
            // (1 + 2 + ... + 10)^2 = 55^2 = 3025
            // Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.
            //
            // Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
            //
            // Basically we need to find a formula which describes the sum of squares and a formula for the sum of natural numbers.
            //
            // There are quite a few sites on the Internet, which gives some very fine proofs for the formulas I am about to give you, so I will cheat a bit on this explanation
            // and just provide the formulas and then link to what I find to be a good proof.
            //
            // We need formulas for two things. The sum of natural numbers, and the sum of squares of natural numbers.
            //
            // The sum of natural numbers can be expressed as:
            //                          S-N = n(n+1) / 2
            // If you want a proof of this formula, go to the following link and check out Proof 1:
            //                          http://www.9math.com/book/sum-first-n-natural-numbers
            //
            // There's a really great story about this formula actually and about how Gauss used it in a school classroom when he was only the age of 10, he was able to solve 1-100
            // in just a couple of seconds. I encourage everyone to look the story up, I believe it's written in "A History of Mathematics" by Carl Boyer.
            //
            // The second formula we need is the sum of squares of natural numbers, which can be expressed as:
            //                          S-N^2 = n(n+1)(2n+1) / 6;
            // If you want a proof of this formula, go to the following link and check out this link:
            //                          http://www.trans4mind.com/personal_development/mathematics/series/sumNaturalSquares.htm
            //
            // Putting these formulas together you get the following code:

            const int n = 100;

            long sum = n*(n + 1)/2; // Formula 1
            long squared = (n*(n + 1)*(2*n + 1))/6; // Formula 2

            long result = sum*sum - squared;

            Console.WriteLine("The difference between the square of sum and sum of squares for 1-100 is "+result);
            Console.ReadLine();

            // This solution gives a running time in O(1), or constant time.
        }
    }
}
