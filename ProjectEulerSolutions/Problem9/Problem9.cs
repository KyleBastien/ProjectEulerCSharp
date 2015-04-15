﻿using System;

namespace Problem9
{
    class Problem9
    {
        static void Main()
        {
            // A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
            //
            // a2 + b2 = c2
            // For example, 32 + 42 = 9 + 16 = 25 = 52.
            //
            // There exists exactly one Pythagorean triplet for which a + b + c = 1000.
            // Find the product abc.
            //
            // No I'm going to skip the Brute Force method here and jump right into the Number Theoretical Approach. This approach is going to be a lot more Mathmatically complex than
            // many other solutions I've written here, so I hope you can hang on and enjoy.
            //
            // More than 2000 years ago Euclid established that Pythagorean Triplets can be generated by the following formula:
            //
            // Given any positive integers m and n where m > n > 0, the integers:
            //                                                      a = m^2 - n^2
            //                                                      b = 2m*n
            //                                                      c = m^2+n^2
            //
            // You can convince your self that (m^2 - n^2)^2 + (2m*n)^2 = (m^2+n^2)^2 by expanding both sides. There are quite a number of other formulas for generating triplets, but this
            // is the one I will use here. Depending on the choices of m and n, we might have to interchnage a and b to adhere to the constraint a < b.
            //
            // Furthermore we need a definition of *primitive*:
            //
            // Definition: Primitive - A Pythagorean Triplet is called primitive if a, b and c are coprime. That means that the greatest common division (gcd) of the set {a,b,c} is one.
            //
            // A Pythagorean triplet is primitive if and only if m and n are coprimes and exactly one of them is even.
            //
            // Proof:
            // Showing that it is a necessary condition is rather easy. Assume that gcd(n,m) = d > 1, then d^2 is a common divisor for {a,b,c} (you can convince yourself about it by inserting
            // m = d*p and n = d*q), and if both n and m are odd then, a,b and c are even, and thus 2 is a common divisior. Thus these conditions are necessary.
            //
            // Showing that they are sufficient is a bit more tricky.
            //
            // Assume that exactly one of m and n is even and triplet is not primitive. Since the triplet is not primitive a prime p is divisible with a, b and c (according to the fundamental
            // theorem of arithmetic as referred to in Problem 3 such a prime exists).
            //
            // From the condition that exactly one of m and n is odd, it follows that a and c is also odd, and so is a p then. Any odd prime that divides b must also divide m and/or n, without
            // loss of generality assume that it divides m. Thus it must also divide m^2. Since the prime is also a divisor of c = m^2 + n^2, then it follows that it must divide n^2 and n as well.
            // (this follows from notions and facts part of the divisor arcticle on wikipedia) Therefore p is a divisor of m and n, and therefore , and n are not coprimes. Thus it is sufficent
            // that the conditions are fulfilled.
            //
            // Every primitive Pythagorean tiplet can be represeted by a unique pair of coprimes m and n where exactly one of them is even. However, not all Pythagoren triplets are generated
            // this way .Approaching it from the other end. From every Pythagorean triplet a primitive triplet can be generated by dividing by the greatest common divisor, thus every Pythagorean
            // triplet can be represented by the unique set {d, m, n} by the given formula.
            //
            //                                                      a = d(m^2 - n^2)
            //                                                      b = 2d*m*n
            //                                                      c = d(m^2+n^2)
            //
            // with m > n > 0, m and n being coprimes and exactly one of m and n is even. d is the greatest common divisor of a, b, and c.
            //
            // Now we have a unique representation of any triplet, but how does that help us solving the problem?
            //
            // Using the conditions:
            //                                                      a + b + c = s = d(m^2 - n^2) + 2d*m*n + d(m^2 + n^2) = 2m(m+n)d
            // We can see that m must be a divisor to s/2, and
            //                                                      m < sqrt(s/2)
            // and we need to find a k = m + n such that k is a divisor to s/(2m) and:
            //
            //                                                      m < k < 2m
            //                                                      k < s/(2m)
            //                                                 k is odd and gcd(m,k) = 1
            //
            // In our problem it means that m < 23, thus decreasing the first number we need to find by a factor 10. The second number k, we need to find has an upper bound depending on m but is in
            // the same vicinity. Once we have found m and k, the rest can be calculated easily. So if we can find a cheap way to calculate teh greatest common divisor, we can make an efficient
            // algorithm for finding the tiplet.
            //
            // First though, we are going to need an algorithm for the greatest common divisor, you can find that in Gcd().
            //
            // We now have all the parts we need to construct an algorithm to find a pythagorean triplet.

            int a = 0, b = 0, c = 0, m;
            int s = 1000;
            bool found = false;

            // ReSharper disable once PossibleLossOfFraction
            int mlimit = (int) Math.Sqrt(d: s/2);
            for (m = 2; m <= mlimit; m++)
            {
                if ((s/2)%m == 0) // m found
                {
                    int k;
                    if (m%2 == 0) // ensure that we find an odd number for k
                    {
                        k = m + 1;
                    }
                    else
                    {
                        k = m + 2;
                    }
                    while (k < 2*m && k <= s/(2*m))
                    {
                        if (s/(2*m)%k == 0 && Gcd(k, m) == 1)
                        {
                            var d = s/2/(k*m);
                            var n = k - m;
                            a = d*(m*m - n*n);
                            b = 2*d*n*m;
                            c = d*(m*m + n*n);
                            found = true;
                            break;
                        }
                        k += 2;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            Console.WriteLine("The pythagorean triple is "+a+", "+b+", "+c+", and the sum is "+s);
            Console.WriteLine("The product is "+(a*b*c));
            Console.ReadLine();
        }

        // Theres lots of different methods for calculating the Greatest Common Divisor, I have chose Euclid's algorithm here.
        public static int Gcd(int a, int b)
        {
            int y;
            int x;

            if (a > b)
            {
                x = a;
                y = b;
            }
            else
            {
                x = b;
                y = a;
            }

            while (x % y != 0)
            {
                int temp = x;
                x = y;
                y = temp%x;
            }
            return y;
        }
    }
}
