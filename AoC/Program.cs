using AoC.HelperMethods;
using AoC._2023.Day01;
using AoC._2023.Day02;
using AoC._2023.Day03;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Reflection;

// Given an array of strings, each string contains 2 numbers, put two numbers together to become a 2-digit number.
// Find the sum of all those numbers.
/*
 Create a new ~~array~~ List of integer.
 Loop through for the first array, loop through each string to put together a 2-digit-number
 Sum them up from the new array
 */

// Get the current directory of the executable (the AoC project directory)
string? currentDirectory = Directory.GetCurrentDirectory();

// Why it says my projectDirectory path is AoC\\bin\\Debug\\net6.0
// ChatGPT note: when you run a .NET application, the working directory might be set to the output directory where the compiled executable is located. In the case of a typical .NET project, this is often the bin\Debug\netX.X or bin\Release\netX.X directory.

// Navigate up the directory structure until you find the directory containing the .csproj file
while (!File.Exists(Path.Combine(currentDirectory, "AoC.csproj")))
{
    currentDirectory = Directory.GetParent(currentDirectory)?.FullName;

    // If there is no .csproj file, break the loop to avoid an infinite loop
    if (currentDirectory == null)
    {
        Console.WriteLine("Error: Could not find the project directory.");
        return;
    }
};

DayOne instanceOne = new DayOne();
DayTwo instanceTwo = new DayTwo();
DayThree instanceThree = new DayThree();


/*//day 1 part 1
string path = HelperMethods.GetRelativePath("Day01", "input.txt");
string[] lines = File.ReadAllLines(path);
int sumPartOne = 0;
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

//day 1 part 2
int sumPartTwo = 0;
string regex = "\\d|one|two|three|four|five|six|seven|eight|nine";
sumPartTwo = DayOne.Solve(lines, regex);
Console.WriteLine(sumPartTwo);*/


/*// Day 2 part 1 + 2
// Specify the relative paths to the input files
string path = Path.Combine(currentDirectory, "2023", "Day02", "input.txt");
string[] lines = File.ReadAllLines(path);

int sum = 0;
int sumOfPowerOfPossibleCubeSets = 0;
DayTwo.PartOne(lines, ref sum);
DayTwo.PartTwo(lines, ref sumOfPowerOfPossibleCubeSets);


Console.WriteLine(sum); // ->2563
Console.WriteLine(sumOfPowerOfPossibleCubeSets); // ->70768

DayTwo instance = new DayTwo();
object partOneLINQ = instance.PartOneLINQ(lines); //object is a reference type that is the base type for all other types, but working with it can require typecasting
object partTwoLINQ = instance.PartTwoLINQ(lines);
Console.WriteLine(partOneLINQ);
Console.WriteLine(partTwoLINQ);
*/

// Day 3
string path = Path.Combine(currentDirectory, "2023", "Day03", "input.txt");
string[] lines = File.ReadAllLines(path);

Dictionary<int, MatchCollection> symbolMatchCollection = instanceThree.GetSymbolInLine(lines);
Dictionary<int, MatchCollection> numbersMatchCollection = instanceThree.GetNumbersInLine(lines);
int sumOfAdjacents = instanceThree.compareSubstringToSymbol(symbolMatchCollection, numbersMatchCollection);
Console.WriteLine(sumOfAdjacents);


//Solution
//char[] Symbols = { '@', '#', '$', '%', '&', '*', '/', '+', '-', '=' };
//string pattern = @"\d+";
//List<string>? list = new List<string>((await File.ReadAllLinesAsync(path)));

//int count = 0;
//for (int row = 0; row < list.Count; row++)
//{
//    for (int col = 0; col < list[row].Length; col++)
//    {
//        if (Symbols.Contains(list[row][col]))
//        {
//            var res = Calculate(list[row - 1], col);
//            res += Calculate(list[row], col);
//            res += Calculate(list[row + 1], col);
//            count += res;
//        }

//    }
//}
//Console.WriteLine(count);

//int Calculate(string line, int col)
//{
//    List<int> indexesToCheck = new List<int> { col - 1, col, col + 1 };
//    int count = 0;
//    MatchCollection matches = Regex.Matches(line, pattern);

//    foreach (Match match in matches)
//    {
//        string number = match.Value;

//        if (AnyIndexInList(indexesToCheck, match.Index, match.Length))
//        {
//            count += Int32.Parse(number);
//        }
//    }
//    return count;
//}

//static bool AnyIndexInList(List<int> list, int startIndex, int length)
//{
//    for (int i = startIndex; i < startIndex + length; i++)
//        if (list.Contains(i))
//            return true;
//    return false;
//}