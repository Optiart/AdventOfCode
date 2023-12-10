namespace Tasks.DayTen
{
    public class PipeMaze
    {
        private const char VerticalPipe = '|';
        private const char HorizontalPipe = '-';
        private const char NorthEastBend = 'L';
        private const char NorthWestBend = 'J';
        private const char SouthWestBend = '7';
        private const char SouthEastBend = 'F';

        private const char Ground = '.';
        private const char Animal = 'S';
        private const char Border = 'b';
        private const char Extended = 'e';

        private readonly HashSet<char> _northDirection = new()
        {
            VerticalPipe, SouthWestBend, SouthEastBend
        };

        private readonly HashSet<char> _westDirection = new()
        {
            HorizontalPipe, NorthEastBend, SouthEastBend
        };

        private readonly HashSet<char> _eastDirection = new()
        {
            HorizontalPipe, NorthWestBend, SouthWestBend
        };

        private readonly HashSet<char> _southDirection = new()
        {
            VerticalPipe, NorthEastBend, NorthWestBend
        };

        private readonly char[,] _map;
        private Point _originalAnimalPoint;

        public PipeMaze(string[] inputLines, bool scaleMap = false)
        {
            if (!scaleMap)
            {
                _originalAnimalPoint = IdentifyAnimalOriginalPoint(inputLines);
                _map = new char[inputLines.Length, inputLines[0].Length];
                for (var i = 0; i < inputLines.Length; i++)
                {
                    for (var j = 0; j < inputLines[i].Length; j++)
                    {
                        _map[i, j] = inputLines[i][j];
                    }
                }
            }
            else
            {
                var map = new char[inputLines.Length * 3, inputLines[0].Length * 3];

                // Init scaled map with extended points
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        map[i, j] = Extended;
                    }
                }

                var originalAnimalPoint = IdentifyAnimalOriginalPoint(inputLines);
                var animalPipe = GetAnimalPipeEquivalent(originalAnimalPoint, inputLines);
                inputLines[originalAnimalPoint.I] = inputLines[originalAnimalPoint.I].Replace(Animal, animalPipe);
                PopulateScaledMap(map, inputLines);

                _originalAnimalPoint = originalAnimalPoint;
                _map = map;
            }
        }

        private Point IdentifyAnimalOriginalPoint(string[] inputLines)
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] == Animal)
                    {
                        return new Point(i, j);
                    }
                }
            }

            throw new Exception("No animal found");
        }

        private char GetAnimalPipeEquivalent(Point originalAnimalPoint, string[] inputLines)
        {
            // determine what kind of pipe is here
            var canGoNorth = _northDirection.Contains(GetTile(inputLines, GetNorthPoint(originalAnimalPoint)));
            var canGoEast = _eastDirection.Contains(GetTile(inputLines, GetEastPoint(originalAnimalPoint)));
            var canGoSouth = _southDirection.Contains(GetTile(inputLines, GetSouthPoint(originalAnimalPoint)));
            var canGoWest = _westDirection.Contains(GetTile(inputLines, GetWestPoint(originalAnimalPoint)));

            if (canGoNorth && canGoEast)
            {
                return NorthEastBend;
            }

            if (canGoNorth && canGoWest)
            {
                return NorthWestBend;
            }

            if (canGoSouth && canGoWest)
            {
                return SouthWestBend;
            }

            if (canGoSouth && canGoEast)
            {
                return SouthEastBend;
            }

            if (canGoNorth || canGoSouth)
            {
                return VerticalPipe;
            }

            return HorizontalPipe;
        }

        private void PopulateScaledMap(char[,] map, string[] inputLines)
        {
            var doubledI = 1;
            var doubledJ = 1;

            for (var i = 0; i < inputLines.Length; i++)
            {
                for (var j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] == Ground)
                    {
                        map[doubledI, doubledJ] = Ground;
                        map[doubledI - 1, doubledJ] = Extended;
                        map[doubledI + 1, doubledJ] = Extended;
                        map[doubledI, doubledJ - 1] = Extended;
                        map[doubledI, doubledJ + 1] = Extended;
                    }
                    else if (inputLines[i][j] == VerticalPipe)
                    {
                        map[doubledI, doubledJ] = VerticalPipe;
                        map[doubledI - 1, doubledJ] = VerticalPipe;
                        map[doubledI + 1, doubledJ] = VerticalPipe;
                        map[doubledI, doubledJ - 1] = Extended;
                        map[doubledI, doubledJ + 1] = Extended;
                    }
                    else if (inputLines[i][j] == HorizontalPipe)
                    {
                        map[doubledI, doubledJ] = HorizontalPipe;
                        map[doubledI - 1, doubledJ] = Extended;
                        map[doubledI + 1, doubledJ] = Extended;
                        map[doubledI, doubledJ - 1] = HorizontalPipe;
                        map[doubledI, doubledJ + 1] = HorizontalPipe;
                    }
                    else if (inputLines[i][j] == NorthEastBend)
                    {
                        map[doubledI, doubledJ] = NorthEastBend;
                        map[doubledI - 1, doubledJ] = VerticalPipe;
                        map[doubledI + 1, doubledJ] = Extended;
                        map[doubledI, doubledJ - 1] = Extended;
                        map[doubledI, doubledJ + 1] = HorizontalPipe;
                    }
                    else if (inputLines[i][j] == NorthWestBend)
                    {
                        map[doubledI, doubledJ] = NorthWestBend;
                        map[doubledI - 1, doubledJ] = VerticalPipe;
                        map[doubledI + 1, doubledJ] = Extended;
                        map[doubledI, doubledJ - 1] = HorizontalPipe;
                        map[doubledI, doubledJ + 1] = Extended;
                    }
                    else if (inputLines[i][j] == SouthWestBend)
                    {
                        map[doubledI, doubledJ] = SouthWestBend;
                        map[doubledI - 1, doubledJ] = Extended;
                        map[doubledI + 1, doubledJ] = VerticalPipe;
                        map[doubledI, doubledJ - 1] = HorizontalPipe;
                        map[doubledI, doubledJ + 1] = Extended;
                    }
                    else if (inputLines[i][j] == SouthEastBend)
                    {
                        map[doubledI, doubledJ] = SouthEastBend;
                        map[doubledI - 1, doubledJ] = Extended;
                        map[doubledI + 1, doubledJ] = VerticalPipe;
                        map[doubledI, doubledJ - 1] = Extended;
                        map[doubledI, doubledJ + 1] = HorizontalPipe;
                    }

                    doubledJ += 3;
                }

                doubledJ = 1;
                doubledI += 3;
            }
        }

        public int CalculateFarthestNumberOfSteps()
        {
            var queue = new Queue<(Point position, int steps)>();
            queue.Enqueue((_originalAnimalPoint, 0));
            var visited = new HashSet<Point>();
            var maxSteps = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current.position))
                {
                    continue;
                }

                visited.Add(current.position);
                maxSteps = Math.Max(maxSteps, current.steps);

                // go north
                if (_northDirection.Contains(GetNorthTile(current.position)))
                {
                    queue.Enqueue((GetNorthPoint(current.position), current.steps + 1));
                }
                // go east
                if (_eastDirection.Contains(GetEastTile(current.position)))
                {
                    queue.Enqueue((GetEastPoint(current.position), current.steps + 1));
                }
                // go south
                if (_southDirection.Contains(GetSouthTile(current.position)))
                {
                    queue.Enqueue((GetSouthPoint(current.position), current.steps + 1));
                }
                // go west
                if (_westDirection.Contains(GetWestTile(current.position)))
                {
                    queue.Enqueue((GetWestPoint(current.position), current.steps + 1));
                }
            }

            return maxSteps;
        }

        public int CalculateEnclosedTiles()
        {
            var scaledStartingPoint = new Point(_originalAnimalPoint.I * 3 + 1, _originalAnimalPoint.J * 3 + 1);
            // Using stack to follow one path - DFS
            var stack = new Stack<(Point position, Point previousPosition)>();
            stack.Push((scaledStartingPoint, scaledStartingPoint));
            var loopPath = new HashSet<Point>();

            // Step 1 -> Find loop
            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (loopPath.Contains(current.position))
                {
                    break;
                }

                loopPath.Add(current.position);

                // go north
                var nextNorthPoint = GetNorthPoint(current.position);
                if (_southDirection.Contains(GetTile(current.position)) &&
                    _northDirection.Contains(GetTile(nextNorthPoint)) &&
                    !nextNorthPoint.Equals(current.previousPosition))
                {
                    stack.Push((nextNorthPoint, current.position));
                }

                // go east
                var nextEastPoint = GetEastPoint(current.position);
                if (_westDirection.Contains(GetTile(current.position)) &&
                    _eastDirection.Contains(GetTile(nextEastPoint)) &&
                    !nextEastPoint.Equals(current.previousPosition))
                {
                    stack.Push((nextEastPoint, current.position));
                }
                // go south
                var nextSouthPoint = GetSouthPoint(current.position);
                if (_northDirection.Contains(GetTile(current.position)) &&
                    _southDirection.Contains(GetTile(nextSouthPoint)) &&
                    !nextSouthPoint.Equals(current.previousPosition))
                {
                    stack.Push((nextSouthPoint, current.position));
                }
                // go west
                var nextWestPoint = GetWestPoint(current.position);
                if (_eastDirection.Contains(GetTile(current.position)) &&
                    _westDirection.Contains(GetTile(nextWestPoint)) &&
                    !nextWestPoint.Equals(current.previousPosition))
                {
                    stack.Push((nextWestPoint, current.position));
                }
            }

            // Use queue to perform BFS
            var queue = new Queue<Point>();
            queue.Enqueue(new Point(0, 0));
            var notEnclosedPoints = new HashSet<Point>();

            // Step 2. Count enclosed tiles
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (notEnclosedPoints.Contains(current) || loopPath.Contains(current))
                {
                    continue;
                }

                notEnclosedPoints.Add(current);

                // go north
                if (GetNorthTile(current) != Border)
                {
                    queue.Enqueue(GetNorthPoint(current));
                }

                // go east
                if (GetEastTile(current) != Border)
                {
                    queue.Enqueue(GetEastPoint(current));
                }

                // go south
                if (GetSouthTile(current) != Border)
                {
                    queue.Enqueue(GetSouthPoint(current));
                }

                // go west
                if (GetWestTile(current) != Border)
                {
                    queue.Enqueue(GetWestPoint(current));
                }
            }

            var grounds = 0;
            var extendedPipes = 0;

            for (var i = 0; i < _map.GetLength(0); i++)
            {
                for (var j = 0; j < _map.GetLength(1); j++)
                {
                    if (loopPath.Contains(new Point(i, j)))
                    {
                        continue;
                    }

                    if (notEnclosedPoints.Contains(new Point(i, j)))
                    {
                        continue;
                    }

                    if (GetTile(new Point(i, j)) == Extended)
                    {
                        continue;
                    }

                    if (GetTile(new Point(i, j)) == Ground)
                    {
                        grounds++;
                    }
                    else
                    {
                        extendedPipes++;
                    }
                }
            }

            return grounds + extendedPipes / 3;
        }

        private char GetNorthTile(Point current) => GetTile(GetNorthPoint(current));

        private Point GetNorthPoint(Point current) => new (current.I - 1, current.J);

        private char GetEastTile(Point current) => GetTile(GetEastPoint(current));

        private Point GetEastPoint(Point current) => new (current.I, current.J + 1);

        private char GetSouthTile(Point current) => GetTile(GetSouthPoint(current));

        private Point GetSouthPoint(Point current) => new (current.I + 1, current.J);

        private char GetWestTile(Point current) => GetTile(GetWestPoint(current));

        private Point GetWestPoint(Point current) => new (current.I, current.J - 1);

        private char GetTile(Point current)
        {
            if (current.I < 0 || current.J < 0 || current.I >= _map.GetLength(0) || current.J >= _map.GetLength(1))
            {
                return Border;
            }

            return _map[current.I, current.J];
        }

        private char GetTile(string[] lines, Point current)
        {
            if (current.I < 0 || current.J < 0 || current.I >= lines.Length || current.J >= lines[current.I].Length)
            {
                return Border;
            }

            return lines[current.I][current.J];
        }

        private void DrawMap(char[,] map)
        {
            Console.WriteLine();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        private void DrawMap(char[,] map, HashSet<Point> loop)
        {
            Console.WriteLine();
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (loop.Contains(new Point(i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(_map[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(_map[i, j]);
                    }
                }

                Console.WriteLine();
            }
        }

        private void DrawMap(char[,] map, HashSet<Point> loop, HashSet<Point> notEnclosed)
        {
            Console.WriteLine();
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (loop.Contains(new Point(i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(_map[i, j]);
                        Console.ResetColor();
                    }
                    else if (notEnclosed.Contains(new Point(i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(_map[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(_map[i, j]);
                    }
                }

                Console.WriteLine();
            }
        }

        private struct Point(int i, int j)
        {
            public int I { get; } = i;

            public int J { get; } = j;
        }
    }
}
