using System;
namespace Problem26
{
    class Problem26
    {
        static void Main()
        {
            // A unit fraction contains 1 in the numerator. The decimal representation of the unit fractions with denominators 2 to 10 are given:
            //
            // 1/2	= 	0.5
            // 1/3	= 	0.(3)
            // 1/4	= 	0.25
            // 1/5	= 	0.2
            // 1/6	= 	0.1(6)
            // 1/7	= 	0.(142857)
            // 1/8	= 	0.125
            // 1/9	= 	0.(1)
            // 1/10	= 	0.1
            // Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.
            //
            // Find the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
            //
            // Let me illustrate with 1/7. First calculation of the remainder of 1/7 gives us 1.
            //
            // Second calculation to analyse the remainder on the first decimal place we multiply by 10, and divide by 7. The remainder of 10/7 is 3.
            //
            // In the third calculation we get 30/7 which gives us a remainder of 2.
            //
            // In the fourth calculation we get 20/7 which gives us a remainder of 6.
            //
            // In the fifth calculation we get 60/7 which gives us a remainder of 4.
            //
            // In the sixth calculation we get 40/7 which gives us a remainder of 5.
            //
            // In the seventh calculation we get 50/7 which gives us a remainder of 1.
            //
            // We already have had a remainder of 1 on the first calculation which means that we continue the calculations we will see the same pattern emerge again, since 10/7 gives us 3 and so on. Thus we have
            // found the longest recurring cycle in 1/7. Or rather we have found the length of the recurring cycle in 1/7 which is 7-1 = 6 digits long.
            //
            // This is a pretty simple solution approach, where all we need to do is to keep calculating the remainder and keep track of the already found remainders.
            //
            // One more thing to note is that the maximum recurring cycle length of 1/d is d-1, as it is pretty obvious from the example. We can get d-1 different possible remainders from the number, since if the
            // result is equal to or greater than d, then it is not a remainder.
            //
            // What that means is that we are more likely to find a large recurring cycle when d is large, and we can stop the search once d becomes lower than the longest recurring cycle we have found.
            //
            // I've chosen to store the already found remainders in an integer array by filling in the position of the remainder in the array index corresponding to the remainder.

            int sequenceLength = 0;
            int num = 0;

            for (int i = 1000; i > 1; i--)
            {
                if (sequenceLength >= i)
                {
                    break;
                }

                int[] foundRemainders = new int[i];
                int value = 1;
                int position = 0;

                while (foundRemainders[value] == 0 && value != 0)
                {
                    foundRemainders[value] = position;
                    value *= 10;
                    value %= i;
                    position++;
                }

                if (position - foundRemainders[value] > sequenceLength)
                {
                    num = i;
                    sequenceLength = position - foundRemainders[value];
                }
            }

            Console.WriteLine("The number with the longest recurring cycle is "+num+", and the cycle is length is "+sequenceLength);
            Console.ReadLine();
        }
    }
}
