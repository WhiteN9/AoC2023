using System.Text.RegularExpressions;

namespace AdventOfCode.DayOne
{
    /*Trebuchet*/
    public class DayOne
    {
        //part 1
        public static int GetSum(int sum, int input)
        {
            return sum + input;
        }

        //part 2
        public static int Solve(string[] lines, string regex)
        {
            //Obtain the data source or sources | lines
            //Create the query
            //Execute the query
            return (
                from string line in lines
                let first = Regex.Match(line, regex)
                let last = Regex.Match(line, regex, RegexOptions.RightToLeft)
                select ParseMatch(first.Value) * 10 + ParseMatch(last.Value)
                ).Sum();
        }

        private static int ParseMatch(string match)
        {
            return match switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                string digit => int.Parse(digit)
            };
        }
    }
}


