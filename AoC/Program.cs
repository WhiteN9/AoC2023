using AdventOfCode;

// Given an array of strings, each string contains 2 numbers, put two numbers together to become a 2-digit number.
// Find the sum of all those numbers.
/*
 Create a new ~~array~~ List of integer.
 Loop through for the first array, loop through each string to put together a 2-digit-number
 Sum them up from the new array
 */

string path = "input.txt";
string[] lines = File.ReadAllLines(path);
int sumPartOne = 0;

//part 1
foreach (string line in lines)
{
    string numbersAsString = "";
    foreach (char ch in line)
    {
        if (char.IsDigit(ch))
        {
            numbersAsString += ch;
        }
    }
    string pair = string.Concat(numbersAsString[0], numbersAsString[numbersAsString.Length - 1]);
    sumPartOne = DayOne.GetSum(sumPartOne, Int32.Parse(pair));
};
Console.WriteLine(sumPartOne);

DayOne dayOne = new DayOne();
//part 2
int sumPartTwo = 0;
string regex = "\\d|one|two|three|four|five|six|seven|eight|nine";
sumPartTwo = dayOne.Solve(lines, regex);
Console.WriteLine(sumPartTwo);