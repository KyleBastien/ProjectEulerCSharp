using System;
namespace Problem15
{
    class Problem15
    {
        static void Main()
        {
            // Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.
            //
            //                          https://projecteuler.net/project/images/p015.gif
            //
            // How many such routes are there through a 20×20 grid?
            //
            // Be sure to click on the link above to see the picture that goes with the question.
            //
            // There are, as to many of the Project Euler problems, lots of different solutions to this. I'm though going to explain the Combinatorics solution though, since I'm really a fan of Combinatorics.
            // 
            // In order to pose the question as a combinatorics question, we must realise a few things. I have generalised the observations to an NxN grid.
            //
            // 1. All paths can be described as a series of directions. And since we can only go down and right, we could describe the paths as a series of Ds and Rs. In a 2x2 grid all paths are 1) DDRR 2) DRDR
            // 3) DRRD 4) RDRD 5) RDDR 6) RRDD.
            //
            // 2. Based on the example we can see that all paths have exactly size 2N of which there are N Rs and N Ds.
            //
            // 3. If we have 2N empty spaces and place all Rs, then the placement of the Ds are given.
            //
            // Once we have made these realisations, we can repose the question as:
            //
            // In how many ways can we choose N out of 2N possible places if the order does not matter?
            //
            // And combinatorics gives us an easy answer to that. The Binomial Coefficient gives us exactly the tool we need to answer the above question. The question is usually posed as:
            //
            // (2N N) = (n k)
            //
            // And using the multiplicative formula we can express it as:
            //
            // (n k) = n(n-1)(n-2)...(n-k+1) / k(k-1)(k-2)...1 = i=1 -> k of n-k+i / i
            //
            // We could also express it as a factorial expression, but that usually gives problems with very large numbers when we try to make the calculations.
            //
            // Wikipedia has a suggested implementation for the multiplicative formula that I have used to get the result so the code is just...

            const int gridSize = 20;
            long paths = 1;

            for (int i = 0; i < gridSize; i++)
            {
                paths *= (2*gridSize) - i;
                paths /= i + 1;
            }

            Console.WriteLine("In a "+gridSize+"x"+gridSize+" grid there are "+paths+" possible paths.");
            Console.ReadLine();
        }
    }
}
