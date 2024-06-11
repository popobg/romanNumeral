using System.Linq;

namespace ChiffresRomains
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static bool isSubstringInDict(string substring, List<string> dictKeys)
            {
                if (dictKeys.Contains(substring))
                {
                    return true;
                }
                return false;
            }

            static bool isRoman(string input)
            {
                try
                {
                    int inputInt = Convert.ToInt32(input);
                    return false;
                }
                catch (Exception)
                {
                    return true;
                }
            }

            static bool checkValidityOfTheList(List<int> arabicNumbers)
            {
                for (int i = 0; i < arabicNumbers.Count() - 1; i++)
                {
                    if (arabicNumbers[i] < arabicNumbers[i + 1])
                    {
                        return false;
                    }
                }
                return true;
            }

            static int CalculateSum(List<int> numbers)
            {
                return numbers.Sum();
            }

            static string ConvertToRoman(int number, Dictionary<int, string> dict)
            {
                string result = "";
                int occurence = 0;

                var reverseKeys = new List<int>();
                reverseKeys = dict.Keys.ToList();
                reverseKeys.Reverse();

                foreach (int checkpoint in reverseKeys)
                {
                    occurence = number / checkpoint;
                    number %= checkpoint;

                    for (int i = 0; i < occurence; i++)
                    {
                        result += dict[checkpoint];
                    }
                }

                return result;
            }

            var romanToArabic = new Dictionary<string, int>
            {
                {"I", 1},
                {"IV", 4},
                {"V", 5},
                {"IX", 9 },
                {"X", 10 },
                {"XL", 40 },
                {"L", 50 },
                {"XC", 90 },
                {"C", 100 },
                {"CD", 400 },
                {"D", 500 },
                {"CM", 900 },
                {"M", 1000 }
            };

            var arabicToRoman = new Dictionary<int, string>
            {
                {1, "I"},
                {4, "IV"},
                {5, "V"},
                {9, "IX"},
                {10, "X"},
                {40, "XL"},
                {50, "L"},
                {90, "XC"},
                {100, "C"},
                {400, "CD"},
                {500, "D"},
                {900, "CM"},
                {1000, "M"}
            };

            while (true)
            {
                Console.WriteLine("Type a number to convert. Type 'quit' or 'q' if you want to close the program.");
                string? userInput = Console.ReadLine()?.ToUpper().Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("You didn't write anything. Try again.");
                    continue;
                }

                // end the program if "quit" is given
                if (userInput == "QUIT" || userInput == "Q")
                {
                    break;
                }

                if (isRoman(userInput))
                {
                    bool validity = true;
                    // decomposition of the roman input with arabic numbers
                    var addition = new List<int>();
                    // check if the previous number was identical
                    int same = 0;

                    for (int i = 0; i < userInput.Length; i++)
                    {
                        string character = userInput[i].ToString();

                        if (!isSubstringInDict(character, romanToArabic.Keys.ToList()))
                        {
                            validity = false;
                            break;
                        }

                        int currentValue = romanToArabic[character];

                        if (i == 0)
                        {
                            // add the arabic value fitting the character
                            addition.Add(currentValue);
                            continue;
                        }

                        int previousValue = romanToArabic[userInput[i - 1].ToString()];
                        if (previousValue > currentValue)
                        {
                            // réfléchir pour choper ici l'exception VIX
                            same = 0;
                            addition.Add(currentValue);
                        }
                        else if (previousValue == currentValue)
                        {
                            if (currentValue == 1 || currentValue == 10 || currentValue == 100 || currentValue == 1000)
                            {
                                same += 1;
                                if (same > 2)
                                {
                                    validity = false;
                                    break;
                                }
                                addition.Add(currentValue);
                            }
                            else
                            {
                                validity = false;
                                break;
                            }
                        }
                        else
                        {
                            // if this is not the end of the user input
                            if (i < userInput.Count() - 1)
                            {
                                if (userInput[i - 1] == userInput[i + 1])
                                {
                                    validity = false;
                                    break;
                                }
                            }

                            string completeCharacter = userInput.Substring(i - 1, 2);

                            same = 0;
                            if (!isSubstringInDict(completeCharacter, romanToArabic.Keys.ToList()))
                            {
                                validity = false;
                                break;
                            }
                            addition.RemoveAt(addition.Count() - 1);
                            addition.Add(romanToArabic[completeCharacter]);
                        }
                    }

                    if (!validity || !checkValidityOfTheList(addition))
                    {
                        Console.WriteLine("This number is not valid. Try again.");
                        continue;
                    }

                    // Sum the decomposition list
                    int result = CalculateSum(addition);
                    Console.WriteLine($"{userInput} = {result}");
                }
                else
                {
                    // try to make a class for "Arabic Numbers" with properties like the dict and method to convert
                    int userInt = Convert.ToInt32(userInput);
                    string result = ConvertToRoman(userInt, arabicToRoman);
                    Console.WriteLine($"{userInt} = {result}");
                }
            }
        }
    }
}