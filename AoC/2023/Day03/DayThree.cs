
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
        Dictionary<int, MatchCollection> gearInLine = new Dictionary<int, MatchCollection>();



        public Dictionary<int, MatchCollection> GetSymbolPerLine(string[] lines)
        {
            string symbol = "[@#$%&*/+\\-=]";
            string symbol2 = "[^0-9.]";
            int lineIndex = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, symbol);
                symbolInLine.Add(lineIndex, matches);
                lineIndex++;
            }
            return symbolInLine;
        }

        public Dictionary<int, MatchCollection> GetPartNumberPerLine(string[] lines)
        {
            string symbol = "[0-9]+";
            string symbol2 = "\\d+";
            int lineIndex = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, symbol);
                numbersInLine.Add(lineIndex, matches);
                lineIndex++;
            }
            return numbersInLine;
        }

        public bool calculateSum(Dictionary<int, MatchCollection> symbolInLine, Match numberString, int currentIndex, ref int sum, bool matchedPartWasFound)
        {
            int number = int.Parse(numberString.Value);

            foreach (Match symbol in symbolInLine[currentIndex])
            {
                if (indexIsWithinValidRange(symbol.Index, numberString.Index, numberString.Length))
                {
                    sum += number;
                    matchedPartWasFound = true;
                    break;
                }
            }
            return matchedPartWasFound;
        }

        public int countPartNumber(Dictionary<int, MatchCollection> symbolInLine, Dictionary<int, MatchCollection> numbersInLine)
        {
            int sumOfAdjacents = 0;
            int currentIndex = 0;
            foreach (MatchCollection lineMatches in numbersInLine.Values)
            {
                if (lineMatches != null)
                {
                    foreach (Match partNumberString in lineMatches)
                    {
                        int partNumber = int.Parse(partNumberString.Value);
                        bool matchedPartWasFound = false;
                        //if the symbol's index is equal or higher to the substring's index-1 AND the symbol's index is less or equal to the substring's index+length+1
                        if (symbolInLine.ContainsKey(currentIndex - 1) && !matchedPartWasFound)
                        {
                            matchedPartWasFound = calculateSum(symbolInLine, partNumberString, currentIndex - 1, ref sumOfAdjacents, matchedPartWasFound);
                        }

                        if (symbolInLine.ContainsKey(currentIndex) && !matchedPartWasFound)
                        {
                            matchedPartWasFound = calculateSum(symbolInLine, partNumberString, currentIndex, ref sumOfAdjacents, matchedPartWasFound);
                        }

                        if (symbolInLine.ContainsKey(currentIndex + 1) && !matchedPartWasFound)
                        {
                            matchedPartWasFound = calculateSum(symbolInLine, partNumberString, currentIndex + 1, ref sumOfAdjacents, matchedPartWasFound);
                        }
                    }
                    currentIndex++;
                }
            }
            return sumOfAdjacents;
        }

        public Dictionary<int, MatchCollection> GetGearSymbolPerLine(string[] lines)
        {
            string symbol = "\\*";
            int lineIndex = 0;
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, symbol);
                gearInLine.Add(lineIndex, matches);
                lineIndex++;
            }
            return gearInLine;
        }

        public bool calculateProduct(Dictionary<int, MatchCollection> numbersInLine, Match gear, int currentIndex, ref List<int> gearPair
, bool matchedGearPairWasFound)
        {

            foreach (Match numberString in numbersInLine[currentIndex])
            {
                if (indexIsWithinValidRange(gear.Index, numberString.Index, numberString.Length))
                {
                    gearPair.Add(int.Parse(numberString.Value));
                }
                if (gearPair.Count == 2)
                {
                    matchedGearPairWasFound = true;
                    break;
                }
            }
            return matchedGearPairWasFound;
        }

        public int countGear(Dictionary<int, MatchCollection> gearInLine, Dictionary<int, MatchCollection> numbersInLine)
        {
            int productOfGears = 0;
            int currentIndex = 0;
            foreach (MatchCollection lineMatches in gearInLine.Values)
            {
                if (lineMatches != null)
                {
                    foreach (Match gear in lineMatches)
                    {
                        List<int> gearPair = new List<int>();
                        bool matchedGearPairWasFound = false;
                        //at the gear Index , search for the 3 indices [x-1,x,x+] around it, across the 3 lines.
                        //the index of (partNumber.index + partNumber.length) is equal to gear, or the index of the substring is equal to the gear, or the index of the substring-1 is equal to the gear

                        if (numbersInLine.ContainsKey(currentIndex - 1) && !matchedGearPairWasFound)
                        {
                            matchedGearPairWasFound = calculateProduct(numbersInLine, gear, currentIndex - 1, ref gearPair, matchedGearPairWasFound);
                        }

                        if (numbersInLine.ContainsKey(currentIndex) && !matchedGearPairWasFound)
                        {
                            matchedGearPairWasFound = calculateProduct(numbersInLine, gear, currentIndex, ref gearPair, matchedGearPairWasFound);
                        }

                        if (numbersInLine.ContainsKey(currentIndex + 1) && !matchedGearPairWasFound)
                        {
                            matchedGearPairWasFound = calculateProduct(numbersInLine, gear, currentIndex + 1, ref gearPair, matchedGearPairWasFound);
                        }

                        if (gearPair.Count == 2)
                        {
                            productOfGears += gearPair[0] * gearPair[1];
                        }
                    }
                    currentIndex++;
                }
            }
            return productOfGears;
        }

        public bool indexIsWithinValidRange(int index, int stringIndex, int stringLength)
        {
            return (index >= stringIndex - 1 && index <= stringIndex + stringLength);
        }
    }
}
