using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DayTwelve
{
    public class HotSprings
    {
        private const char Operational = '.';
        private const char Damaged = '#';
        private const char Unknown = '?';

        private readonly ConditionRecord[] _records;

        public HotSprings(string[] inputLines, bool isFolded = false)
        {
            _records = new ConditionRecord[inputLines.Length];

            if (isFolded)
            {
                for (var i = 0; i < inputLines.Length; i++)
                {
                    var parts = inputLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var foldedRecord = $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}";
                    var foldedGrouping = $"{parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";

                    _records[i] = new ConditionRecord
                    {
                        Line = foldedRecord,
                        GroupOfDamaged = foldedGrouping.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                    };
                }
            }
            else
            {
                for (var i = 0; i < inputLines.Length; i++)
                {
                    var parts = inputLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    _records[i] = new ConditionRecord
                    {
                        Line = parts[0],
                        GroupOfDamaged = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                    };
                }
            }
        }

        public long CalculateSumOfArrangements()
        {
            long sum = 0;
            for (int i = 0; i < _records.Length; i++)
            {
                var result = CalculateArrangement(_records[i]);
                Console.WriteLine($"Processed {i + 1} with result: {result}");
                sum += result;
            }

            return sum;
        }

        private int CalculateArrangement(ConditionRecord record)
        {
            var memo = new Dictionary<string, int>();
            //Console.WriteLine(record.Line);
            //var result = CalculateArrangements(record.Line, 0, "", record.GroupOfDamaged, memo);
            var result = CalculateArrangementsWithQueue(record.Line, record.GroupOfDamaged);
            return result;
        }

        private int CalculateArrangementsWithQueue(string input, int[] group)
        {
            var queue = new Queue<(string str, int i)>();
            queue.Enqueue(("", 0));
            var sum = 0;
            var window = group.Sum() + group.Length - 1;

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (current.str.Length == input.Length)
                {
                    if (IsValidLine(current.str, group))
                    {
                        sum++;
                    }

                    continue;
                }

                if (IsCompletelyInvalidLine(current.str, group))
                {
                    continue;
                }

                var j = current.i;
                while (j < input.Length && input[j] != Unknown)
                {
                    current.str += input[j];
                    j++;
                }

                if (j < input.Length)
                {
                    queue.Enqueue((current.str + Damaged, j + 1));
                    queue.Enqueue((current.str + Operational, j + 1));
                }
                else
                {
                    queue.Enqueue((current.str, j));
                }
            }

            return sum;
        }

        //private int CalculateArrangements(string input, int currentPosition, string converted, int[] group, Dictionary<string, int> memo)
        //{
        //    Console.WriteLine(converted);

        //    if (memo.ContainsKey(converted))
        //    {
        //        Console.WriteLine($"Already processed:{converted}");
        //        return memo[converted];
        //    }

        //    if (IsCompletelyInvalidLine(converted, group))
        //    {
        //        return 0;
        //    }

        //    if (input.Length == converted.Length)
        //    {
        //        if (IsValidLine(converted, group))
        //        {
        //            return 1;
        //        }

        //        return 0;
        //    }

        //    var leftSum = 0;
        //    var rightSum = 0;

        //    var nextConvertedLine = converted;
        //    for (var i = currentPosition; i < input.Length; i++)
        //    {
        //        if (input[i] == Unknown || input[i] == Damaged)
        //        {
        //            //Console.WriteLine($"Building new for # : {nextConvertedLine}");
        //            leftSum += CalculateArrangements(input, i + 1, converted + Damaged, group, memo);
        //        }

        //        if (input[i] == Unknown || input[i] == Operational)
        //        {
        //            //Console.WriteLine($"Building new for . : {nextConvertedLine}");
        //            rightSum += CalculateArrangements(input, i + 1, converted + Operational, group, memo);
        //        }
        //    }

        //    memo[converted] = leftSum + rightSum;
        //    return leftSum + rightSum;
        //}

        private bool IsCompletelyInvalidLine(string line, int[] group)
        {
            var damaged = 0;
            var groupIndex = 0;

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == Damaged)
                {
                    damaged++;

                    // We found extra group
                    if (groupIndex == group.Length)
                    {
                        return true;
                    }

                    // More damaged than in a group
                    if (damaged > group[groupIndex])
                    {
                        return true;
                    }

                    continue;
                }

                if (damaged > 0)
                {
                    // Less damaged than in a group
                    if (damaged < group[groupIndex])
                    {
                        return true;
                    }

                    // Group is fulfilled -> move on
                    if (damaged == group[groupIndex])
                    {
                        damaged = 0;
                        groupIndex++;
                    }
                }
            }

            return false;
        }

        private bool IsValidLine(string line, int[] group)
        {
            var damaged = 0;
            var groupIndex = 0;

            for (var i = 0; i <= line.Length; i++)
            {
                if (i == line.Length || line[i] != Damaged)
                {
                    if (damaged > 0)
                    {
                        if (groupIndex == group.Length)
                        {
                            return false;
                        }

                        if (group[groupIndex] != damaged)
                        {
                            return false;
                        }

                        damaged = 0;
                        groupIndex++;
                    }

                    continue;
                }

                if (line[i] == Damaged)
                {
                    damaged++;
                }
            }

            return groupIndex == group.Length;
        }

        private class ConditionRecord
        {
            public string Line { get; set; }

            public int[] GroupOfDamaged { get; set; }
        }
    }
}
