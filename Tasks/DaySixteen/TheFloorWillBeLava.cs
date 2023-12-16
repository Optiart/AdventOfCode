using Tasks.Helpers;
using Tasks.Shared;

namespace Tasks.DaySixteen
{
    using static Movement;

    public class TheFloorWillBeLava(string[] inputLines)
    {
        private readonly char[,] _initialMap = InputReader.ReadToCharMap(inputLines);

        private const char VerticalSplitter = '|';
        private const char HorizontalSplitter = '-';
        private const char MirrorLeansRight = '/';
        private const char MirrorLeansLeft = '\\';
        private const char EmptySpace = '.';

        public int DetermineMaxEnergizedTileCount_BruteForce()
        {
            var startingMovements = new List<Movement>();
            var rows = _initialMap.GetLength(0);
            var columns = _initialMap.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                startingMovements.Add(new Movement(new Point(i, 0), Right));
            }

            for (var j = 0; j < columns; j++)
            {
                startingMovements.Add(new Movement(new Point(0, j), Down));
            }

            for (var i = rows - 1; i >= 0; i--)
            {
                startingMovements.Add(new Movement(new Point(i, columns - 1), Left));
            }

            for (var j = _initialMap.GetLength(1) - 1; j >= 0; j--)
            {
                startingMovements.Add(new Movement(new Point(rows - 1, j), Up));
            }

            return startingMovements.Max(m => DetermineEnergizedTileCount(m.Position, m.Direction));
        }

        public int DetermineEnergizedTileCount(Point startPoint = default, char startDirection = Right)
        {
            var stack = new Stack<Movement>();
            var energized = new HashSet<Point>();
            var visited = new HashSet<Movement>();
            var start = new Movement(startPoint, startDirection);
            stack.Push(start);

            while (stack.Count > 0)
            {
                var currentMove = stack.Pop();

                if (!CanMove(_initialMap, currentMove) || visited.Contains(currentMove))
                {
                    continue;
                }

                energized.Add(currentMove.Position);
                visited.Add(currentMove);

                var nextMovements = GetNextMovements(_initialMap, currentMove);
                foreach (var nextMovement in nextMovements)
                {
                    stack.Push(nextMovement);
                }
            }

            return energized.Count;
        }

        private Movement[] GetNextMovements(char[,] map, Movement currentMove) =>
            map[currentMove.Position.I, currentMove.Position.J] switch
            {
                VerticalSplitter when currentMove.Direction is Right or Left => new[] { currentMove.MoveUp(), currentMove.MoveDown() },
                VerticalSplitter or EmptySpace when currentMove.Direction is Up => new[] { currentMove.MoveUp() },
                VerticalSplitter or EmptySpace when currentMove.Direction is Down => new[] { currentMove.MoveDown() },

                HorizontalSplitter when currentMove.Direction is Up or Down => new[] { currentMove.MoveLeft(), currentMove.MoveRight() },
                HorizontalSplitter or EmptySpace when currentMove.Direction is Left => new[] { currentMove.MoveLeft() },
                HorizontalSplitter or EmptySpace when currentMove.Direction is Right => new[] { currentMove.MoveRight() },

                MirrorLeansRight when currentMove.Direction is Right => new[] { currentMove.MoveUp() },
                MirrorLeansRight when currentMove.Direction is Left => new[] { currentMove.MoveDown() },
                MirrorLeansRight when currentMove.Direction is Up => new[] { currentMove.MoveRight() },
                MirrorLeansRight when currentMove.Direction is Down => new[] { currentMove.MoveLeft() },

                MirrorLeansLeft when currentMove.Direction is Right => new[] { currentMove.MoveDown() },
                MirrorLeansLeft when currentMove.Direction is Left => new[] { currentMove.MoveUp() },
                MirrorLeansLeft when currentMove.Direction is Up => new[] { currentMove.MoveLeft() },
                MirrorLeansLeft when currentMove.Direction is Down => new[] { currentMove.MoveRight() },

                _ => throw new NotSupportedException("Tile is not supported")
            };

        private bool CanMove(char[,] map, Movement current) =>
            current.Position.I >= 0 &&
            current.Position.I < map.GetLength(0) &&
            current.Position.J >= 0 &&
            current.Position.J < map.GetLength(1);
    }
}
