
using System.Text.RegularExpressions;

namespace AoC._2023.Day03
{
    public class DayThree
    {
        /*
         * Get the location of the special symbols in each line
         * i.e line -1 - null
         * i.e line 0 - {3,6,9}
         * i.e line 1 - {3,6,9}
         * Numbers of line 0 is cross checked against line -1, 0, 1
         * Numbers of line 1 is cross checked against line 0, 1, 2
         * i.e One substring integer is at index 3, length 3. The symbol index has to be between 2 and 7 for it to be count as an adjacent number.
         * On line 1, if there is value at index 
         * 
         * 
         */

        Dictionary<int, MatchCollection> symbolInLine = new Dictionary<int, MatchCollection>();
        Dictionary<int, MatchCollection> numbersInLine = new Dictionary<int, MatchCollection>();


        public Dictionary<int, MatchCollection> GetSymbolInLine(string[] lines)
        {
            string symbol = "[^0-9.]";
            int lineIndex = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, symbol);
                symbolInLine.Add(lineIndex, matches);
                lineIndex++;
            }
            return symbolInLine;
        }

        public Dictionary<int, MatchCollection> GetNumbersInLine(string[] lines)
        {
            string symbol = "[0-9]+";
            int lineIndex = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, symbol);
                numbersInLine.Add(lineIndex, matches);
                lineIndex++;
            }
            return numbersInLine;
        }

        public int compareSubstringToSymbol(Dictionary<int, MatchCollection> symbolInLine, Dictionary<int, MatchCollection> numbersInLine)
        {
            int sumOfAdjacents = 0;
            int currentIndex = 0;
            foreach (MatchCollection lineMatches in numbersInLine.Values)
            {
                if (lineMatches != null)
                {
                    foreach (Match numberSubString in lineMatches.Cast<Match>())
                    {
                        int convertedInteger = int.Parse(numberSubString.Value);
                        MatchCollection symbolLine1 = null;
                        MatchCollection symbolLine2 = null;
                        MatchCollection symbolLine3 = null;
                        //if the symbol's index is equal or higher to the substring's index-1 AND the symbol's index is less or equal to the substring's index+length+1
                        if (symbolInLine.ContainsKey(currentIndex - 1))
                        {
                            symbolLine1 = symbolInLine[currentIndex - 1];
                            foreach (Match symbol in symbolLine1)
                            {
                                if (symbol.Index >= numberSubString.Index - 1 && symbol.Index <= (numberSubString.Index + numberSubString.Length + 1))
                                {
                                    sumOfAdjacents += convertedInteger;
                                    break;
                                }
                            }
                        }
                        if (symbolInLine.ContainsKey(currentIndex))
                        {
                            symbolLine2 = symbolInLine[currentIndex];
                            foreach (Match symbol in symbolLine2)
                            {

                                if (symbol.Index >= numberSubString.Index - 1 && symbol.Index <= numberSubString.Index + numberSubString.Length + 1)
                                {
                                    sumOfAdjacents += convertedInteger;
                                    break;
                                }
                            }
                        }
                        if (symbolInLine.ContainsKey(currentIndex + 1))
                        {
                            symbolLine3 = symbolInLine[currentIndex + 1];
                            foreach (Match symbol in symbolLine3)
                            {
                                if (symbol.Index >= numberSubString.Index - 1 && symbol.Index <= numberSubString.Index + numberSubString.Length + 1)
                                {
                                    sumOfAdjacents += convertedInteger;
                                    break;
                                }
                            }
                        }
                    }
                    currentIndex++;
                }
            }
            return sumOfAdjacents;
        }
    }
}
