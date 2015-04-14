using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem8
{
    class Problem8
    {
        static void Main()
        {
            // The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
            //
            // 73167176531330624919225119674426574742355349194934
            // 96983520312774506326239578318016984801869478851843
            // 85861560789112949495459501737958331952853208805511
            // 12540698747158523863050715693290963295227443043557
            // 66896648950445244523161731856403098711121722383113
            // 62229893423380308135336276614282806444486645238749
            // 30358907296290491560440772390713810515859307960866
            // 70172427121883998797908792274921901699720888093776
            // 65727333001053367881220235421809751254540594752243
            // 52584907711670556013604839586446706324415722155397
            // 53697817977846174064955149290862569321978468622482
            // 83972241375657056057490261407972968652414535100474
            // 82166370484403199890008895243450658541227588666881
            // 16427171479924442928230863465674813919123162824586
            // 17866458359124566529476545682848912883142607690042
            // 24219022671055626321111109370544217506941658960408
            // 07198403850962455444362981230987879927244284909188
            // 84580156166097919133875499200524063689912560717606
            // 05886116467109405077541002256983155200055935729725
            // 71636269561882670428252483600823257530420752963450
            //
            // Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?
            //
            // Thank God for Ctrl + K, Ctrl + C, right?
            //
            // Today's solution is going to be a little bit *different*. Basically, I'm not sure the best approach to take here, this Problem is really just a giant number
            // crunching problem and I don't see a faster approach than to brute force it. This sucks though as I hate to just brute force a problem, so I am also going to
            // try my hand at writing a functional solution to the problem and compare the two solutions to see if there is any noticable advantage.
            //
            // Look to the functional() method to see the functional solution and look to the bruteForce() method to see the brute force solution.

            DateTime startTime = DateTime.Now;
            Functional();
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            Console.WriteLine("Functional Took: "+duration.TotalMilliseconds);
            Console.ReadLine();

            startTime = DateTime.Now;
            BruteForce();
            endTime = DateTime.Now;
            duration = endTime - startTime;

            Console.WriteLine("Brute Force took: "+duration.TotalMilliseconds);
            Console.ReadLine();

            // From results here it appears that the Functional solution is slower, but only by *roughly* 10 milliseconds, I ran this program about 30 times all with similar results
            // the Functional solution took about 20 ms and the BruteForce solution finish quick enough that my PC is registering 10 ms. One could also agrue that the BruteForce
            // solution is a lot easier to read and understand than the Functional solution as well so I really think the BruteForce is the solution to go with here.
            //
            // WOOOO!! I just had to rewrite a bunch of the code because I used int's not long's as it turns out with a thirteen digit sum you overflow an int and I didn't realize
            // until I went to submit to Project Euler. I have corrected this though in this code, make sure to watch out for that yourself!
            //
            // I would though love to know if someone has derived a solution that is faster than the one I have provided here though, feel free to open an issue or submit a Pull Request.
        }

        public static void Functional()
        {
            // Basically what this is going to do is have a 5 digit window that pans across the entire large number and when we get to end report the largest product that we
            // find. The first problem that we run across is that we need to store a 1,000 digit number, and there is no way C# can deal with it as an actual number, so instead
            // we will store it as a string.

            const string input = @"73167176531330624919225119674426574742355349194934
                                    96983520312774506326239578318016984801869478851843
                                    85861560789112949495459501737958331952853208805511
                                    12540698747158523863050715693290963295227443043557
                                    66896648950445244523161731856403098711121722383113
                                    62229893423380308135336276614282806444486645238749
                                    30358907296290491560440772390713810515859307960866
                                    70172427121883998797908792274921901699720888093776
                                    65727333001053367881220235421809751254540594752243
                                    52584907711670556013604839586446706324415722155397
                                    53697817977846174064955149290862569321978468622482
                                    83972241375657056057490261407972968652414535100474
                                    82166370484403199890008895243450658541227588666881
                                    16427171479924442928230863465674813919123162824586
                                    17866458359124566529476545682848912883142607690042
                                    24219022671055626321111109370544217506941658960408
                                    07198403850962455444362981230987879927244284909188
                                    84580156166097919133875499200524063689912560717606
                                    05886116467109405077541002256983155200055935729725
                                    71636269561882670428252483600823257530420752963450";

            // To create our List of digits we break up the string into individual characters with ToCharArray(). The Where clause filters out any new line or space characters from the
            // sequence (they're there because of the way I've formatted the string). The the Select clause converts each character into its corresponding number. Finally the ToList()
            // method causes the sequence of integers to be evaluated and put into a List.

            List<long> digits = input.ToCharArray().Where(char.IsDigit).Select(c => long.Parse(c.ToString())).ToList();

            // Use our RangeFrom extension method we can jump in at a particular index, and create a query expression to create the quintuplets. This is actually a very fast way to create
            // our "window".

            IEnumerable<IEnumerable<long>> quintupletsSelector = from index in 0.To(digits.Count - 13)
                                     select digits.RangeFrom(index, 13);

            quintupletsSelector.Select(quintuplet => quintuplet.Product()).Max().Display();
        }

        public static void BruteForce()
        {
            // The logic here is very simple, just loop through the sequence of numbers, calculating the product and checking it if is larger than the previously largest found. 

            const string input = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

            long largest = 0;

            for (int i = 0; i < input.Length - 12; i++)
            {
                long num = long.Parse(input.Substring(i, 1))*
                          long.Parse(input.Substring(i + 1, 1)) *
                          long.Parse(input.Substring(i + 2, 1)) *
                          long.Parse(input.Substring(i + 3, 1)) *
                          long.Parse(input.Substring(i + 4, 1)) *
                          long.Parse(input.Substring(i + 5, 1)) *
                          long.Parse(input.Substring(i + 6, 1)) *
                          long.Parse(input.Substring(i + 7, 1)) *
                          long.Parse(input.Substring(i + 8, 1)) *
                          long.Parse(input.Substring(i + 9, 1)) *
                          long.Parse(input.Substring(i + 10, 1)) *
                          long.Parse(input.Substring(i + 11, 1)) *
                          long.Parse(input.Substring(i + 12, 1));
                if (num > largest)
                {
                    largest = num;
                }
            }

            Console.WriteLine("The largest product is: "+largest);
        }
    }

    public static class Extensions
    {
        public static IEnumerable<long> To(this int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        public static IEnumerable<long> RangeFrom(this IList<long> list, long startIndex, int count)
        {
            for (long i = startIndex; i < startIndex + count; i++)
            {
                yield return list[(int)i];
            }
        }

        public static long Product(this IEnumerable<long> factors)
        {
            return factors.Aggregate<long, long>(1, (current, factor) => current*factor);
        }

        public static void Display(this object result)
        {
            Console.WriteLine("The largest product is: "+result);
        }
    }
}
