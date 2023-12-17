using System.Text.RegularExpressions;

namespace AdventOfCode.DayTwo
{
    /*Cube Conundrum*/
    public class DayTwo
    {
        public DayTwo()
        {
            /* 
             * We have 3 types of colored marbles, each come in a different quantity.
             * We have a list of games, each game has a unique ID, there are 3 sets of the marbles, each set containing a random quantity of marbles. 
             * 
             * Going through each game, determine if we have enough marbles to play the game.
             * 
             * Given {green:5, blue:1, red:5}
             * Example 1 -  Game 1: [{green:1, blue:2, red:3},{green:3, blue:2, red:1},{green:1, blue:1, red:1}]
             * We won't have enough blue to play this game
             * 		 * 
             */

            //With the game ID being the key, iterate through the substrings of the string array
            //The value of the key can be an array with 3 indices, [0,0,0]
            //Each time we iterate through a substring, we use regex to find the digit and to find the word
            //If the word is red, we compare with the existing value and place the highest value in the first index
            //If the word is green, we compare with the existing value and place the highest value in the second index
            //If the word is blue, we compare with the existing value and place the highest value in the third index
        }

        public static void PartOneAndTwo2(string[] lines, ref int sum, ref int sumOfPowerOfPossibleCubeSets)
        {
            foreach (string line in lines)
            {
                Dictionary<int, int[]> highestScoreOfEachTeamPerGame = new Dictionary<int, int[]>(); //int[] {red,green,blue}

                char[] teamPointDelimiters = new char[] { ',', ';' };

                string[] separatedMatchIDAndMatchDetail = line.Split(':', StringSplitOptions.TrimEntries);
                int gameID = int.Parse(Regex.Match(separatedMatchIDAndMatchDetail[0], "\\d+").Value);
                string[] pointAndTeamPairs = separatedMatchIDAndMatchDetail[1].Split(teamPointDelimiters, StringSplitOptions.TrimEntries);

                highestScoreOfEachTeamPerGame.Add(gameID, new int[] { 0, 0, 0 });
                //find the max cube cost of each game
                foreach (var pointOfTeam in pointAndTeamPairs)
                {
                    int points = int.Parse(Regex.Match(pointOfTeam, "\\d+").Value);
                    string teamName = Regex.Match(pointOfTeam, "red|blue|green").Value;

                    if (teamName == "red" && points > highestScoreOfEachTeamPerGame[gameID][0])
                    {
                        highestScoreOfEachTeamPerGame[gameID][0] = points;
                    }
                    if (teamName == "green" && points > highestScoreOfEachTeamPerGame[gameID][1])
                    {
                        highestScoreOfEachTeamPerGame[gameID][1] = points;
                    }
                    if (teamName == "blue" && points > highestScoreOfEachTeamPerGame[gameID][2])
                    {
                        highestScoreOfEachTeamPerGame[gameID][2] = points;
                    }
                }

                //which games would have been possible if the bag had been loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes
                int[] val = highestScoreOfEachTeamPerGame[gameID];

                sumOfPowerOfPossibleCubeSets += val[0] * val[1] * val[2];
                //if the game requires less cubes than the given amount of cubes (per color), we can play the game
                if (val[0] <= 12 && val[1] <= 13 && val[2] <= 14)
                {
                    sum += gameID;
                }
                highestScoreOfEachTeamPerGame.Clear();
            }
        }

        public static void PartOne(string[] lines, ref int sum)
        {
            foreach (string line in lines)
            {
                DayTwo instance = new DayTwo();
                Dictionary<int, int[]> highestScoreOfEachTeamPerGame = instance.ParseGame(line);

                // Which games would have been possible if the bag had been loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes
                int gameID = highestScoreOfEachTeamPerGame.Keys.First();
                int[] val = highestScoreOfEachTeamPerGame.Values.First();

                // If the game requires less cubes than the given amount of cubes (per color), we can play the game
                if (val[0] <= 12 && val[1] <= 13 && val[2] <= 14)
                {
                    sum += gameID;
                }
            }
        }

        //the limitation of using dictionary is, it sucks, just do 4 int instead.
        public static void PartTwo(string[] lines, ref int sumOfPowerOfPossibleCubeSets)
        {
            foreach (string line in lines)
            {
                DayTwo instance = new DayTwo();
                Dictionary<int, int[]> highestScoreOfEachTeamPerGame = instance.ParseGame(line);

                int[] val = highestScoreOfEachTeamPerGame.Values.First();

                sumOfPowerOfPossibleCubeSets += val[0] * val[1] * val[2];
            }
        }

        public Dictionary<int, int[]> ParseGame(string line) =>
            new Dictionary<int, int[]>()
            {
                {
                    ParseInts(line, @"Game (\d+)").First(),
                    new int[] {
                        ParseInts(line, @"(\d+) red").Max(),
                        ParseInts(line, @"(\d+) green").Max(),
                        ParseInts(line, @"(\d+) blue").Max()
                    }
                }
            };

        //Regex.Matches - The result is a collection of Match objects.
        // range variable 'match' that iterates over each Match object

        //IEnumerable<int> ParseInts(string line, string regex)
        //{
        //    IEnumerable<int> ints = Enumerable.Empty<int>();
        //    ints = (
        //        from match in Regex.Matches(line, regex) 
        //        select int.Parse(match.Groups[1].Value)
        //    );
        //    return ints;
        //}

        IEnumerable<int> ParseInts(string line, string regex) =>
                from match in Regex.Matches(line, regex)
                select int.Parse(match.Groups[1].Value);

        public object PartOneLINQ(string[] lines) => (
            from line in lines
            let game = ParseGame(line)
            where game.Values.First()[0] <= 12 && game.Values.First()[1] <= 13 && game.Values.First()[2] <= 14
            select game.Keys.First()
        ).Sum();

        public object PartOneLINQMethodSyntax(string[] lines)
        {
            return lines
                .Select(line => ParseGame(line))
                .Where(game => game.Values.First()[0] <= 12 && game.Values.First()[1] <= 13 && game.Values.First()[2] <= 14)
                .Select(game => game.Keys.First())
                .Sum();
        }

        public object PartTwoLINQ(string[] lines) => (
            from line in lines
            let game = ParseGame(line)
            select game.Values.First()[0] * game.Values.First()[1] * game.Values.First()[2]
        ).Sum();
    }
}
