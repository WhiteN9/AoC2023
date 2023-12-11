
using System.Collections;

namespace AdventOfCode.DayTwo;
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
		 * 
		 * Game 1: 1 red, 10 blue, 5 green; 11 blue, 6 green; 6 green; 1 green, 1 red, 12 blue; 3 blue; 3 blue, 4 green, 1 red
		 * 
		 * Break into
		 * <1> <
		 */
		string a = "Game 31: 9 blue; 1 red, 2 blue, 5 green; 2 blue, 2 red, 9 green; 2 blue, 1 red, 8 green; 11 green, 2 red, 3 blue; 7 green, 5 blue";
		var b = a.Split(": ");
        Dictionary<int, IList> jikan = new Dictionary<int, IList>();

    }
}
