using System;
using System.IO;
namespace Problem18
{
    class Problem18
    {
        static void Main()
        {
            // By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.
            //
            //                                                                     3
            //                                                                    7 4
            //                                                                   2 4 6
            //                                                                  8 5 9 3
            //
            // That is, 3 + 7 + 4 + 9 = 23.
            //
            // Find the maximum total from top to bottom of the triangle below:
            //
            //                                                                      75
            //                                                                     95 64
            //                                                                    17 47 82
            //                                                                   18 35 87 10
            //                                                                  20 04 82 47 65
            //                                                                 19 01 23 75 03 34
            //                                                                88 02 77 73 07 63 67
            //                                                               99 65 04 28 06 16 70 92
            //                                                              41 41 26 56 83 40 80 70 33
            //                                                             41 48 72 33 47 32 37 16 94 29
            //                                                            53 71 44 65 25 43 91 52 97 51 14
            //                                                           70 11 33 28 77 73 17 78 39 68 17 57
            //                                                          91 71 52 38 17 14 91 43 58 50 27 29 48
            //                                                         63 66 04 68 89 53 67 30 73 16 69 87 40 31
            //                                                        04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
            //
            // NOTE: As there are only 16384 routes, it is possible to solve this problem by trying every route. However, Problem 67, is the same challenge with a triangle containing one-hundred rows; it cannot 
            // be solved by brute force, and requires a clever method! ;o)
            // 
            // I thought I should explain how I decided to store the triange of data first. Basically the data will look like:
            //
            //                                                                       3 0 0 0
            //                                                                       7 4 0 0
            //                                                                       2 4 6 0
            //                                                                       8 5 9 3
            //
            // in terms of the triangle going right increases the index by one, and going left keeps the index, and the triangle is padded with zeros.
            //
            // I also didn't want to input all this data manually, and I like things I can reuse, so I wrote a readInput function which reads the data from a input.txt file that you can find the debug folder of this
            // project.
            // 
            // The given problem is a classical exmaple of dynamic programming, and it really works well fo rit. The methodology is a bit more complex than the brute force solution, but I will take a shot at explaning
            // it anyway. It's probably easiest with an example so I'm going to use the four line example at the top of the question.
            //
            // Standing at the top of the triange we have to choose between going left and right. In order to make the optimal choice (which maximizes the sum), we would have to know how large a sum we can get if we
            // go either way. So in order to answer the question we would basically have to solve smaller versions of the same problem, imagine two triangles around the numbers one row under the top row.
            //
            // We can break each of the sub-problems down in a similar way, and we can continue to do so until we reach a sub-problem at the bottom line consisting of only one number, then the question becomes trivial
            // to answer, since the answer is the number it self. Once that question is answered we can move up one row, and answer the question posed there with a solution which is a + max(b,c).
            //
            // Once we know the answer to all 3 sub-problems on the next to last line, we can move up and answer the posed sub-problems by the same formula already applied. And we can continue to so until we reach
            // the original question of whether to go left or right.
            //
            // If we break down the problem into sub-problems we can see that breaking the left-three-row triangle into two and breaking the right-three-row triangle into two would yield us 4 triangles, or sub-problems.
            // However the sub-problem in the overlapping part is identical. In this problem solving the sub-problem yields the same result no matter how we reached it. This is fairly easy to see in our example. When
            // a problem has this property it is said to have optimal sub-structure. Since we have a problem with optimal sub structure we only have to solve three sub-problems in the next to bottom line, and therefore 
            // the dynamic programming solution is effective.
            //
            // It can be proven that the problem has an optimal sub-structure. However, I won't go into the details of that here, but leave that as an open end you can pick up and explore.
            //
            // If we want to solve the small problem with brute force, we would need to test all 8 paths, each resulting in 3 additions, in total 24 additions.
            //
            // If we use dynamic programming, the first iteration would require 3 maximum comparison operations and 3 additions. The next line would require 2 maximum comparison operations and 2 additions, and the first
            // line would require one of each. So a total of 6 maximum comparison operations and 6 additions.
            //
            // For small problems this saving is small if any at all, but for a problem with 15 lines, solving the first iteration and brute forcing from there would reduce the number of brute force additions
            // from 15*2^14 to 14*2^13 a saving of approximately 131,000 fewer additions, at the cost of 15 additions and 15 maximum comparison operations. That is a pretty good improvment.
            //
            // We can make a short-cut with the algorithm, as we don't have to break the problem into sub-problems, but can start from the bottom and work the way up through the triangle until we reach the top and
            // the algorithm spits out a number.
            //
            // We start with a triangle that looks like:
            //                                                                         3
            //                                                                        7 4
            //                                                                       2 4 6
            //                                                                      8 5 9 3
            //
            // Applying the algorithm to the small problem we will need three iterations. The first iteration we apply the rule a + max(b,c) which creates a new triangle which looks as:
            //
            //                                                                         3
            //                                                                        7 4
            //                                                                      10 13 15
            //
            // Making the second iteration of the algorithm makes the triangle look:
            //                                                                         3
            //                                                                      20  19
            //
            // And if we run the algorithm once more, the triangle collapses to one number - 23- which is the answer to the small example.
            //
            // The algorithm looks a little something like this..

            string filename = "input.txt";
            int[,] inputTriangle = ReadInput(filename);
            int lines = inputTriangle.GetLength(0);
            for (int i = lines - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    inputTriangle[i, j] += Math.Max(inputTriangle[i + 1, j], inputTriangle[i + 1, j + 1]);
                }
            }

            Console.WriteLine("The largest sum through the triangle is: "+inputTriangle[0,0]);
            Console.ReadLine();
        }

        public static int[,] ReadInput(string filename)
        {
            string line;
            int lines = 0;

            StreamReader r = new StreamReader(filename);
            while ((r.ReadLine()) != null)
            {
                lines++;
            }

            int[,] inputTriangle = new int[lines, lines];
            r.BaseStream.Seek(0, SeekOrigin.Begin);

            int j = 0;
            while ((line = r.ReadLine()) != null)
            {
                var linePieces = line.Split(' ');
                for (int i = 0; i < linePieces.Length; i++)
                {
                    inputTriangle[j, i] = int.Parse(linePieces[i]);
                }
                j++;
            }
            r.Close();
            return inputTriangle;
        }
    }
}
