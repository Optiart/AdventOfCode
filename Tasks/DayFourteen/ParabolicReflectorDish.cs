namespace Tasks.DayFourteen
{
    public class ParabolicReflectorDish
    {
        private const char Rock = '#';
        private const char RoundRock = 'O';
        private const char Ground = '.';

        private readonly char[,] _initialMap;

        public ParabolicReflectorDish(string[] inputLines)
        {
            _initialMap = new char[inputLines.Length, inputLines[0].Length];
            for (int i = 0; i < inputLines.Length; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    _initialMap[i, j] = inputLines[i][j];
                }
            }
        }

        public int CalculateNorthLoadWithCycled()
        {
            var initialMap = _initialMap;
            int latestResult = 0;
            for (int i = 0; i < 1000; i++)
            {
                var northMap = GoNorth(initialMap);
                var westMap = GoWest(northMap);
                var southMap = GoSouth(westMap);
                initialMap = GoEast(southMap);

                latestResult = CalculateNorthLoad(initialMap);
            }

            return latestResult;
        }

        private char[,] GoNorth(char[,] map)
        {
            var newMap = new char[map.GetLength(0), map.GetLength(1)];
            var blockedAt = new int[map.GetLength(0)];

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == Rock)
                    {
                        blockedAt[j] = i + 1;
                        newMap[i, j] = Rock;
                    }
                    else if (map[i, j] == RoundRock)
                    {
                        newMap[blockedAt[j], j] = RoundRock;

                        if (i != blockedAt[j])
                        {
                            newMap[i, j] = Ground;
                        }

                        blockedAt[j]++;
                    }
                    else
                    {
                        newMap[i, j] = Ground;
                    }
                }
            }

            return newMap;
        }

        private char[,] GoSouth(char[,] map)
        {
            var newMap = new char[map.GetLength(0), map.GetLength(1)];
            var blockedAt = new int[map.GetLength(0)];
            for (int i = 0; i < blockedAt.Length; i++)
            {
                blockedAt[i] = map.GetLength(1) - 1;
            }

            for (var i = map.GetLength(0) - 1; i >= 0; i--)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == Rock)
                    {
                        blockedAt[j] = i - 1;
                        newMap[i, j] = Rock;
                    }
                    else if (map[i, j] == RoundRock)
                    {
                        newMap[blockedAt[j], j] = RoundRock;

                        if (i != blockedAt[j])
                        {
                            newMap[i, j] = Ground;
                        }

                        blockedAt[j]--;
                    }
                    else
                    {
                        newMap[i, j] = Ground;
                    }
                }
            }

            return newMap;
        }

        private char[,] GoWest(char[,] map)
        {
            var newMap = new char[map.GetLength(0), map.GetLength(1)];
            var blockedAt = new int[map.GetLength(0)];

            for (var j = 0; j < map.GetLength(1); j++)
            {
                for (var i = 0; i < map.GetLength(0); i++)
                {
                    if (map[i, j] == Rock)
                    {
                        blockedAt[i] = j + 1;
                        newMap[i, j] = Rock;
                    }
                    else if (map[i, j] == RoundRock)
                    {
                        newMap[i, blockedAt[i]] = RoundRock;

                        if (j != blockedAt[i])
                        {
                            newMap[i, j] = Ground;
                        }

                        blockedAt[i]++;
                    }
                    else
                    {
                        newMap[i, j] = Ground;
                    }
                }
            }

            return newMap;
        }

        private char[,] GoEast(char[,] map)
        {
            var newMap = new char[map.GetLength(0), map.GetLength(1)];
            var blockedAt = new int[map.GetLength(0)];
            for (int i = 0; i < blockedAt.Length; i++)
            {
                blockedAt[i] = map.GetLength(1) - 1;
            }

            for (var j = map.GetLength(1) - 1; j >= 0; j--)
            {
                for (var i = 0; i < map.GetLength(0); i++)
                {
                    if (map[i, j] == Rock)
                    {
                        blockedAt[i] = j - 1;
                        newMap[i, j] = Rock;
                    }
                    else if (map[i, j] == RoundRock)
                    {
                        newMap[i, blockedAt[i]] = RoundRock;

                        if (j != blockedAt[i])
                        {
                            newMap[i, j] = Ground;
                        }

                        blockedAt[i]--;
                    }
                    else
                    {
                        newMap[i, j] = Ground;
                    }
                }
            }

            return newMap;
        }

        private void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write($"{map[i, j]}");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public int CalculateNorthLoad()
        {
            return CalculateNorthLoad(GoNorth(_initialMap));
        }

        public int CalculateNorthLoad(char[,] map)
        {
            var result = 0;

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == RoundRock)
                    {
                        result += map.GetLength(0) - i;
                    }
                }
            }

            return result;
        }
    }
}
