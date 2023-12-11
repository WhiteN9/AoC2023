using AoC.HelperMethods;
using AdventOfCode.DayOne;
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
string currentDirectory = Directory.GetCurrentDirectory();

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

////day 1 part 1
//string path = HelperMethods.GetRelativePath("Day01","input.txt");

//string[] lines = File.ReadAllLines(path);
//int sumPartOne = 0;
//foreach (string line in lines)
//{
//    string numbersAsString = "";
//    foreach (char ch in line)
//    {
//        if (char.IsDigit(ch))
//        {
//            numbersAsString += ch;
//        }
//    }
//    string pair = string.Concat(numbersAsString[0], numbersAsString[numbersAsString.Length - 1]);
//    sumPartOne = DayOne.GetSum(sumPartOne, Int32.Parse(pair));
//};
//Console.WriteLine(sumPartOne);

////day 1 part 2
//int sumPartTwo = 0;
//string regex = "\\d|one|two|three|four|five|six|seven|eight|nine";
//sumPartTwo = DayOne.Solve(lines, regex);
//Console.WriteLine(sumPartTwo);


//With the game ID being the key, iterate through the substrings of the string array
//The value of the key can be an array with 3 indices, [0,0,0]
//Each time we iterate through a substring, we use regex to find the digit and to find the word
//If the word is red, we compare with the existing value and place the highest value in the first index
//If the word is green, we compare with the existing value and place the highest value in the second index
//If the word is blue, we compare with the existing value and place the highest value in the third index

// Day 2 part 1

// Specify the relative paths to the input files
string path = Path.Combine(currentDirectory, "2023", "Day02", "input.txt");
string[] lines = File.ReadAllLines(path);

Dictionary<int, int[]> highestScoreOfEachTeamPerGame = new Dictionary<int, int[]>(); //int[] {red,green,blue}

foreach (string line in lines)
{
    char[] teamPointDelimiters = new char[] { ',', ';' };

    string[] separatedMatchIDAndMatchDetail = line.Split(':', StringSplitOptions.TrimEntries);
    int matchID = int.Parse(Regex.Match(separatedMatchIDAndMatchDetail[0], "\\d+").Value);
    string[] pointAndTeamPairs = separatedMatchIDAndMatchDetail[1].Split(teamPointDelimiters, StringSplitOptions.TrimEntries);

    highestScoreOfEachTeamPerGame.Add(matchID, new int[] { 0, 0, 0 });
    foreach (var pointOfTeam in pointAndTeamPairs)
    {
        int points = int.Parse(Regex.Match(pointOfTeam, "\\d+").Value);
        string teamName = Regex.Match(pointOfTeam, "red|blue|green").Value;

        if (teamName == "red" && points > highestScoreOfEachTeamPerGame[matchID][0])
        {
            highestScoreOfEachTeamPerGame[matchID][0] = points;
        }
        if (teamName == "green" && points > highestScoreOfEachTeamPerGame[matchID][1])
        {
            highestScoreOfEachTeamPerGame[matchID][1] = points;
        }
        if (teamName == "blue" && points > highestScoreOfEachTeamPerGame[matchID][2])
        {
            highestScoreOfEachTeamPerGame[matchID][2] = points;
        }
    }
}
//which games would have been possible if the bag had been loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes
int sum = 0;
int sumOfPowerOfPossibleCubeSets = 0;
foreach (int key in highestScoreOfEachTeamPerGame.Keys)
{
    int[] val = highestScoreOfEachTeamPerGame[key];

    sumOfPowerOfPossibleCubeSets += val[0] * val[1] * val[2]; 
    //if the game requires less cubes than the given amount of cubes (per color), we can play the game
    if (val[0] <= 12 && val[1] <= 13 && val[2] <= 14)
    {
        sum += key;
    }
}

Console.WriteLine(sum);
Console.WriteLine(sumOfPowerOfPossibleCubeSets);



