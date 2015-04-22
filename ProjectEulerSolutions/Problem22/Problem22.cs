using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Problem22
{
    class Problem22
    {
        static void Main()
        {
            // Using names.txt (right click and 'Save Link/Target As...'), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical 
            // value for each name, multiply this value by its alphabetical position in the list to obtain a name score.
            //
            // For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.
            //
            // What is the total of all the name scores in the file?
            //
            // Basically, this problem can be broken down to three different parts...
            //
            // 1. Read the input file and turn the data into a manageable data structure
            // 2. Sort the data
            // 3. Sum of and provide the answer
            //
            // Reading a file for its content is a simple procedure which we have done before, but in order to get the data into a usable format, we need to make a bit of effore about the data structure in the 
            // file. Go to ReadInput function for more details.
            //
            // We also need a method to find the sum of an individual name, this is relatively easy. Go to Sum function for more details.
            //
            // With these two functions we can write some of our main code....

            string filename = "names.txt";
            string[] names = ReadInput(filename);

            // Now, I'm always someone to use the .NET functions whenever I can, so instead of implementing my own sorting algorithm I'm going to use the Array.Sort() method. *But* there is a slight problem with that.
            // Basically the Array.Sort() comparer can treat strings with AA in them as the Danish/Norwegian letter Å, which comes at the end of the alphabet, and thus messes up the sorting. This is only true though
            // if your program is running in a Danish locale, *I think*, just to be sure though I decided to ues the InvariantCulture Comparer, which is also part of the .NET framework. This *should* product the
            // correct results no matter what locale the program is run in.

            Array.Sort(names, StringComparer.InvariantCulture);

            // With all of those pieces in place we can write a very simple summation LINQ expression to sum up all of the names.

            int result = names.Select((t, i) => (i + 1)*Sum(t)).Sum();

            Console.WriteLine("The sum of all names are: "+result);
            Console.ReadLine();
        }

        // I converted each character to the ASCII value, which for A is 65, B is 66 and so on. So by subtracting 64, you get the value asked for in the problem description. 
        public static int Sum(string name)
        {
            return name.Sum(t => Convert.ToInt32(t) - 64);
        }

        // Some notes about the file we are reading in....
        // 1. No line breaks
        // 2. Names are encapsulated in quotation marks - these needs to be stripped
        // 3. All names are separated by a comma.
        // 4. All names seems to be completely in upper case
        //
        // Basically with these assumptions its very easy to make a method that takes in the file and returns a string array. All we did was split it around the comma and trim off the quotation marks.
        public static string[] ReadInput(string filename)
        {
            StreamReader r = new StreamReader(filename);
            string line = r.ReadToEnd();
            r.Close();

            string[] names = line.Split(',');

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Trim('"');
            }

            return names;
        }
    }
}
