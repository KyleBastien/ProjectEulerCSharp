using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem24
{
    class Problem24
    {
        static void Main()
        {
            // A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, 
            // we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:
            //
            // 012   021   102   120   201   210
            //
            // What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
            //
            // Basically what this solution leverages on is the fact that there are n! permutations when we have n elements to be ordered.
            //
            // First we want to figure out, which number is first in the millionth lexicographical permutation. The last nine digist can be ordred in 9! = 362880 ways. So the first 362880 permutations start with a
            // 0. The permutations from 362881 to 725760 start with 1 and then the permutations from 725761 to 1088640 starts with a 2. Based on that it is clear that the millionth permutaiton is the third interval,
            // and thus must start with a 2.
            //
            // So the 725761st permutation is 2013456789. So now we miss 274239 permutations to reach the goal, so we can start figuring out the second number.
            //
            // Since 8! = 40320, we can get that changing the number six times we reach the permutations from 241920 - 282240. So we need to take the 7th number in the list. Since the list excludes 2 we end up with 7
            // as the second digit of the millionth permutation. We can then continue all the way through until we got all digits.
            //
            // I haven't found a factorial function in the .NET API, :(, so I wrote one. See Factor. There is probably a better implementation than the one I wrote but we only need to use small factorials so it fits our
            // needs well.
            //
            // We break the loop once we reach zero remaining permutations. The last part of the algorithm appends the rest of the digits in case we break out of the main algorithm.
            
            int[] perm = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            int n = perm.Length;
            string permNum = "";
            int remain = 1000000 - 1;

            List<int> numbers = new List<int>();
            for (int i = 0; i < n; i++)
            {
                numbers.Add(i);
            }

            for (int i = 1; i < n; i++)
            {
                int j = remain/Factor(n - i);
                remain = remain%Factor(n - i);
                permNum = permNum + numbers[j];
                numbers.RemoveAt(j);
                if (remain == 0)
                {
                    break;
                }
            }

            permNum = numbers.Aggregate(permNum, (current, t) => current + t);

            Console.WriteLine("The millionth lexicographic permutation is: "+permNum);
            Console.ReadLine();
        }

        public static int Factor(int i)
        {
            if (i < 0)
            {
                return 0;
            }

            int p = 1;
            for (int j = 1; j <= i; j++)
            {
                p *= j;
            }
            return p;
        }
    }
}
