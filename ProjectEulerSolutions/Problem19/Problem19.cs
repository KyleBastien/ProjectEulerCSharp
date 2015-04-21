using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem19
{
    class Problem19
    {
        static void Main()
        {
            // You are given the following information, but you may prefer to do some research for yourself.
            //
            // 1 Jan 1900 was a Monday.
            // Thirty days has September,
            // April, June and November.
            // All the rest have thirty-one,
            // Saving February alone,
            // Which has twenty-eight, rain or shine.
            // And on leap years, twenty-nine.
            // A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
            //
            // How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
            //
            // For this solution I'm going to stick to utilizing the .NET DateTime API since I'm sure that this API is very well optimizied, far beyond any code I've written in this repo. Noting that the solution
            // is rather simple, two for loops to reach all the dates and then check if that is Sunday.

            int sundays = 0;

            for (int year = 1901; year <= 2000; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if ((new DateTime(year, month, 1)).DayOfWeek == DayOfWeek.Sunday)
                    {
                        sundays++;
                    }
                }
            }

            // I'm not completly happy with this solution though because it has a runtime of O(n^2) and I really would like to reduce this down to O(n), it just so happens in this case that there are not a lot of
            // Dates to check. 1 date for every month between 1901 and 2000, thats roughly 99 days to check.

            Console.WriteLine("There are "+sundays+" sundays on the first of a month.");
            Console.ReadLine();
        }
    }
}
