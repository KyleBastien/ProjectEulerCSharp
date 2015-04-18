using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
namespace Problem17
{
    class Problem17
    {
        static void Main()
        {
            // If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
            //
            // If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?
            //
            //
            // NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. The use of "and" when writing out 
            // numbers is in compliance with British usage.
            //
            // So this problem is actually rather difficult. So I've had to make an entire OTHER class for this problem. This class is going to act as an Extension so I can convert a number into a words. Go ahead
            // and jump over there and read the comments so you can get an idea of what it's doing.
            //
            // With the ConvertToWordsExtension in place it's actually trivial to solve this problem...

            Console.WriteLine("The number of letters used in the numbers from 1 to 1000 are: "+1.To(1000)
                    .Select(number => number.ConvertToWords().ToCharArray().Count(char.IsLetter))
                    .Sum());
            Console.ReadLine();
        }
    }

    // Basically how this extension works is the following: 
    // 1. Let n be the number to process
    // 2. For numbers smaller than 0, combine "Negative" with the result of calling the algorithm on the absolute value of n
    // 3. For n between 0 and 20, look up the result in the word dictionary
    // 4. For n between 20 and 100, calculate the number of 10s and number of units; look up the name for the number of tens; if there are any units, look up the appropriate name and combine the two results
    // with a hyphen
    // 5. For n between 100 and 1000, calculate the number of 100s and the remainder; look up the name of the number of hundreds; if there is any remainder, recurse to get its wordy representation, then combine
    // it with result for the hundreds using "and" to join the two parts
    // 6. For n bigger than 1000: decide which is the biggest named number (million, billion, etc.) that divides into n. Calculate the number of that base unit and remainder. Recurse to convert the number of base
    // units into words, and recurse to convert the remainder into words. Combine the two parts using ","
    public static class ConvertToWordsExtension
    {
        private static Dictionary<long, string> _wordDictionary;
        private const int OneThousand = 1000;
        private const long OneMillion = 1000000;
        private const long OneBillion = 1000000000;
        private const long OneTrillion = 1000000000000;
        private const long OneQuadrillion = 1000000000000000;
        private const long OneQuintillion = 1000000000000000000;

        // Converts a number to its English representation in words.
        public static string ConvertToWords(this long number)
        {
            EnsureWordDictionaryInitialised();

            if (number == long.MinValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return ConvertToWordsCore(number);
        }

        // Converts a number to its English representation in words.
        public static string ConvertToWords(this int number)
        {
            return ConvertToWords((long)number);
        }

        private static Dictionary<long, string> CreateWordDictionary()
        {
            return new Dictionary<long, string>
            {
                {0, "zero"},
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"},
                {10, "ten"},
                {11, "eleven"},
                {12, "twelve"},
                {13, "thirteen"},
                {14, "fourteen"},
                {15, "fifteen"},
                {16, "sixteen"},
                {17, "seventeen"},
                {18, "eighteen"},
                {19, "nineteen"},
                {20, "twenty"},
                {30, "thirty"},
                {40, "forty"},
                {50, "fifty"},
                {60, "sixty"},
                {70, "seventy"},
                {80, "eighty"},
                {90, "ninety"},
                {100, "hundred"},
                {OneThousand, "thousand"},
                {OneMillion, "million"},
                {OneBillion, "billion"},
                {OneTrillion, "trillion"},
                {OneQuadrillion, "quadrillion"},
                {OneQuintillion, "quintillion"}
            };
        } 

        // 
        private static void EnsureWordDictionaryInitialised()
        {
            // Ensuer thread-safety when caching our word dictionary. Note: This doesn't prevent two copies of the word dictionary being initialised - but that doesn't matter; only one would be cached, the
            // other garbage collected.
            if (_wordDictionary == null)
            {
                var dictionary = CreateWordDictionary();
                Interlocked.CompareExchange(ref _wordDictionary, dictionary, null);
            }
        }

        private static string ConvertToWordsCore(long number)
        {
            return
             number < 0 ? "negative " + ConvertToWordsCore(Math.Abs(number))
             : 0 <= number && number < 20 ? _wordDictionary[number]
             : 20 <= number && number < 100 ? ProcessTens(number, _wordDictionary)
             : 100 <= number && number < OneThousand ? ProcessHundreds(number, _wordDictionary)
             : OneThousand <= number && number < OneMillion ? ProcessLargeNumber(number, OneThousand, _wordDictionary)
             : OneMillion <= number && number < OneBillion ? ProcessLargeNumber(number, OneMillion, _wordDictionary)
             : OneBillion <= number && number < OneTrillion ? ProcessLargeNumber(number, OneBillion, _wordDictionary)
             : OneTrillion <= number && number < OneQuadrillion ? ProcessLargeNumber(number, OneTrillion, _wordDictionary)
             : OneQuadrillion <= number && number < OneQuintillion ? ProcessLargeNumber(number, OneQuadrillion, _wordDictionary)
             : ProcessLargeNumber(number, OneQuintillion, _wordDictionary); // long.Max value is just over nine quintillion
        }

        private static string ProcessLargeNumber(long number, long baseUnit, Dictionary<long, string> wordDictionary)
        {
            // split the number into number of baseUnits (thousands, millions, etc.) and the remainder
            var numberOfBaseUnits = number/baseUnit;
            var remainder = number%baseUnit;

            // apply ConvertToWordsCore to represent the number of baseUnits as words
            string conversion = ConvertToWordsCore(numberOfBaseUnits) + " " + wordDictionary[baseUnit];
            // recurse for any remainder
            conversion += remainder <= 0 ? "" : (remainder < 100 ? " and " : ", ") + ConvertToWordsCore(remainder);
            return conversion;
        }

        private static string ProcessHundreds(long number, Dictionary<long, string> wordDictionary)
        {
            var hundreds = number/100;
            var remainder = number%100;
            string conversion = wordDictionary[hundreds] + " " + wordDictionary[100];
            conversion += remainder > 0 ? " and " + ConvertToWordsCore(remainder) : "";
            return conversion;
        }

        private static string ProcessTens(long number, Dictionary<long, string> wordDictionary)
        {
            Debug.Assert(0 <= number && number < 100);

            // Split the number into the number of tens and the number of units, so that words for both can be looked up independantly
            var tens = (number/10)*10;
            var units = number%10;
            string conversion = wordDictionary[tens];
            conversion += units > 0 ? "-" + wordDictionary[units] : "";
            return conversion;
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
    }
}
