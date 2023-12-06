namespace Tasks.DayThree
{
    public class GearRatios
    {
        private static bool ShouldCount(char c) => c != '.' && !char.IsDigit(c);

        public static int CalculateSum(string[] map)
        {
            var sum = 0;
            var num = string.Empty;
            var count = false;

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (char.IsDigit(map[i][j]))
                    {
                        num += map[i][j];

                        if (!count)
                        {
                            if (i - 1 >= 0) // top
                            {
                                if (ShouldCount(map[i - 1][j]))
                                {
                                    count = true;
                                }
                            }
                            if (i - 1 >= 0 && j - 1 >= 0) // top left
                            {
                                if (ShouldCount(map[i - 1][j - 1]))
                                {
                                    count = true;
                                }
                            }
                            if (j - 1 >= 0) // left
                            {
                                if (ShouldCount(map[i][j - 1]))
                                {
                                    count = true;
                                }
                            }
                            if (i + 1 < map.Length && j - 1 >= 0) // bottom left
                            {
                                if (ShouldCount(map[i + 1][j - 1]))
                                {
                                    count = true;
                                }
                            }
                            if (i + 1 < map.Length) // bottom
                            {
                                if (ShouldCount(map[i + 1][j]))
                                {
                                    count = true;
                                }
                            }
                            if (i + 1 < map.Length && j + 1 < map[i].Length) // bottom right
                            {
                                if (ShouldCount(map[i + 1][j + 1]))
                                {
                                    count = true;
                                }
                            }
                            if (j + 1 < map[i].Length) // right
                            {
                                if (ShouldCount(map[i][j + 1]))
                                {
                                    count = true;
                                }
                            }
                            if (i - 1 >= 0 && j + 1 < map[i].Length) // top right
                            {
                                if (ShouldCount(map[i - 1][j + 1]))
                                {
                                    count = true;
                                }
                            }
                        }
                    }

                    if (!char.IsDigit(map[i][j]) || j + 1 >= map[i].Length)
                    {
                        if (count)
                        {
                            sum += int.Parse(num);
                            count = false;
                        }

                        num = string.Empty;
                    }
                }
            }

            return sum;
        }

        public static int CalculateRatioSum(string[] map)
        {
            var num = string.Empty;
            var numberPosition = new Dictionary<(int i, int j), Number>();

            for (int i = 0; i < map.Length; i++)
            {
                var coordinatesToPush = new Queue<(int i, int j)>();
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (char.IsDigit(map[i][j]))
                    {
                        num += map[i][j];
                        coordinatesToPush.Enqueue((i, j));
                    }

                    if (!char.IsDigit(map[i][j]) || j + 1 >= map[i].Length)
                    {
                        var number = new Number();

                        while (coordinatesToPush.Any())
                        {
                            number.Value = int.Parse(num);
                            var coordinate = coordinatesToPush.Dequeue();
                            numberPosition[coordinate] = number;
                        }

                        num = string.Empty;
                    }
                }
            }

            var adjustmentCount = 0;
            var sum = 0;
            var ratio = 1;

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '*')
                    {
                        if (i - 1 >= 0) // top
                        {
                            if (GetValue((i - 1, j), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (i - 1 >= 0 && j - 1 >= 0) // top left
                        {
                            if (GetValue((i - 1, j - 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (j - 1 >= 0) // left
                        {
                            if (GetValue((i, j - 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (i + 1 < map.Length && j - 1 >= 0) // bottom left
                        {
                            if (GetValue((i + 1, j - 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (i + 1 < map.Length) // bottom
                        {
                            if (GetValue((i + 1, j), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (i + 1 < map.Length && j + 1 < map[i].Length) // bottom right
                        {
                            if (GetValue((i + 1, j + 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (j + 1 < map[i].Length) // right
                        {
                            if (GetValue((i, j + 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                        if (i - 1 >= 0 && j + 1 < map[i].Length) // top right
                        {
                            if (GetValue((i - 1, j + 1), out var value))
                            {
                                adjustmentCount++;
                                ratio *= value;
                            }
                        }
                    }

                    if (map[i][j] == '*' || j + 1 >= map[i].Length)
                    {
                        if (adjustmentCount == 2)
                        {
                            sum += ratio;
                        }

                        adjustmentCount = 0;
                        ratio = 1;
                    }
                }
            }

            bool GetValue((int i, int j) coordinates, out int value)
            {
                value = default;
                numberPosition.TryGetValue((coordinates.i, coordinates.j), out var number);
                if (number is null || number.IsTaken)
                {
                    return false;
                }

                value = number.Value;
                number.IsTaken = true;
                return true;
            }
            return sum;
        }

        public class Number
        {
            public bool IsTaken { get; set; }

            public int Value { get; set; }

            public override string ToString()
            {
                return $"{Value}, {IsTaken}";
            }
        }
    }
}