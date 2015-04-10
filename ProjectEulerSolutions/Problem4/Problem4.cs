using System;
using System.Linq;

namespace Problem4
{
    class Problem4
    {
        static void Main()
        {
            // A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
            //
            // Find the largest palindrome made from the product of two 3-digit numbers.
            //
            // Theres two basic ways you could solve this problem: 
            // 1) We could start multiplying 3-digit numbers and then check if the result is a laindrome.
            // 2) We can make palindromes and see if we can find two 3-digit numbers which are the factor pair we are looking for.
            //
            // I'm going to focus on #2, under the impression that there are less palindromes than 3-digit numbers, but I could be totally wrong and 
            // they could be the exact same.
            //
            // If we square 999, we will have the largest possible number as a product of two 3-digit numbers. 999 * 999 = 998001. The largest possible
            // palindrome which can be made which is less than that, must be 997799. The smallest possible palindrome we can encounter is 11111. Under
            // the assumption that we will only have to deal with even digit plaindromes a helper function for constructing palindromes is fairly simple.
            // *See makePalindrome*
            //
            // Once we have obtained a palindrome number (p), we need to check if a factor pair (i, j) of 3-digit numbers exists. In the previous problem
            // we established that i in such a factor pair must be larger than the square root of the palindrome number, and as a result j must be lower.
            // So we can limit our search for i knowing that we only have to check numbers between 999 and the square root of the palindrome number. Furthermore
            // we can limit our search by checking that j = p / i < 1000, since it means that the proposed solution no longer consists of two 3-digit numbers,
            // which was one of the requirements.
            //
            // Now we have dervied a set of stopping conditions for the search, all we need to do now, is to check if i is a factor to the palindorme. This can be
            // done with my beloved modulo operator.

            bool found = false;
            int firstHalf = 998, palindrome = 0;
            int[] factors = new int[2];

            while (!found)
            {
                firstHalf--;
                palindrome = MakePalindrome(firstHalf);
                for (int i = 999; i > 99; i--)
                {
                    if ((palindrome/i) > 999 || i*i < palindrome)
                    {
                        break;
                    }

                    if ((palindrome%i == 0))
                    {
                        found = true;
                        factors[0] = palindrome/i;
                        factors[1] = i;
                        break;
                    }
                }
            }

            Console.WriteLine("Found the palindrome "+palindrome+", which is made from "+factors[0]+" * "+factors[1]);
            Console.ReadLine();
        }

        private static int MakePalindrome(int firstHalf)
        {
            char[] reversed = firstHalf.ToString().Reverse().ToArray();
            return Convert.ToInt32(firstHalf + new string(reversed));
        }
    }
}
