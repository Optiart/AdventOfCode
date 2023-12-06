public class Trebuchet
{
    private static Dictionary<string, int> _startStringNumbers = new()
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };

    private static Dictionary<string, int> _endStringNumbers = new()
    {
        {"eno", 1},
        {"owt", 2},
        {"eerht", 3},
        {"ruof", 4},
        {"evif", 5},
        {"xis", 6},
        {"neves", 7},
        {"thgie", 8},
        {"enin", 9}
    };

    public static int GetSumOfCalibrationValues(string[] inputValues, bool includeStringNumbers) =>
        includeStringNumbers
            ? inputValues.Sum(GetCalibrationValueIncludingString)
            : inputValues.Sum(GetCalibrationValue);

    public static int GetCalibrationValue(string input)
    {
        var startIndex = 0;
        var endIndex = input.Length - 1;
        var start = -1;
        var end = -1;

        while (startIndex <= endIndex)
        {
            if (start == -1)
            {
                if (char.IsDigit(input[startIndex]))
                {
                    start = (int)char.GetNumericValue(input[startIndex]) * 10;
                }
                else
                {
                    startIndex++;
                }
            }

            if (end == -1)
            {
                if (char.IsDigit(input[endIndex]))
                {
                    end = (int)char.GetNumericValue(input[endIndex]);
                }
                else
                {
                    endIndex--;
                }
            }

            if (end != -1 && start != -1)
            {
                return start + end;
            }
        }

        throw new Exception("No solution exist for the input");
    }

    public static int GetCalibrationValueIncludingString(string input)
    {
        var startIndex = 0;
        var endIndex = input.Length - 1;
        var start = -1;
        var end = -1;
        var startWord = "";
        var endWord = "";

        while (startIndex <= input.Length || endIndex >= -1)
        {
            if (start == -1)
            {
                var foundWordNumber = FindNumberInWord(startWord, isForward: true);
                if (foundWordNumber != -1)
                {
                    start = foundWordNumber;
                }
                else if (char.IsDigit(input[startIndex]))
                {
                    start = (int)char.GetNumericValue(input[startIndex]);
                }
                else
                {
                    startWord += input[startIndex];
                    startIndex++;
                }
            }

            if (end == -1)
            {
                var foundWordNumber = FindNumberInWord(endWord, isForward: false);
                if (foundWordNumber != -1)
                {
                    end = foundWordNumber;
                }
                else if (char.IsDigit(input[endIndex]))
                {
                    end = (int)char.GetNumericValue(input[endIndex]);
                }
                else
                {
                    endWord += input[endIndex];
                    endIndex--;
                }
            }

            if (end != -1 && start != -1)
            {
                return start * 10 + end;
            }
        }

        throw new Exception("No solution exist for the input");
    }

    public static int FindNumberInWord(string word, int startIndex = 0, int length = 1, bool isForward = true)
    {
        if (startIndex == word.Length)
        {
            return -1;
        }

        if (startIndex + length > word.Length)
        {
            return FindNumberInWord(word, ++startIndex, 1, isForward);
        }

        var part = word.Substring(startIndex, length);

        if (isForward && _startStringNumbers.ContainsKey(part))
        {
            return _startStringNumbers[part];
        }
        else if (!isForward && _endStringNumbers.ContainsKey(part))
        {
            return _endStringNumbers[part];
        }
        else if (part.Length == 5)
        {
            return FindNumberInWord(word, ++startIndex, 1, isForward);
        }
        else
        {
            return FindNumberInWord(word, startIndex, ++length, isForward);
        }
    }
}