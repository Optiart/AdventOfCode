namespace Tasks.DayEleven
{
    public class CosmicExpansion
    {
        private const char Galaxy = '#';

        private readonly HashSet<int> _emptyRows = new();
        private readonly HashSet<int> _emptyColumns = new();
        private readonly List<Point> _galaxies = new();

        public CosmicExpansion(string[] inputLines)
        {
            var isRowEmpty = true;

            for (var i = 0; i < inputLines.Length; i++)
            {
                for (var j = 0; j < inputLines[i].Length; j++)
                {
                    if (inputLines[i][j] == Galaxy)
                    {
                        _galaxies.Add(new Point(i, j));
                        isRowEmpty = false;
                    }
                }

                if (isRowEmpty)
                {
                    _emptyRows.Add(i);
                }
                isRowEmpty = true;
            }

            var isColumnEmpty = true;
            for (var j = 0; j < inputLines[0].Length; j++)
            {
                for (var i = 0; i < inputLines.Length; i++)
                {
                    if (isColumnEmpty && inputLines[i][j] == Galaxy)
                    {
                        isColumnEmpty = false;
                    }
                }

                if (isColumnEmpty)
                {
                    _emptyColumns.Add(j);
                }
                isColumnEmpty = true;
            }
        }

        public long FindShortestPathSum(int expansionDistance = 2)
        {
            long sum = 0;
            for (var i = 0; i < _galaxies.Count; i++)
            {
                var startGalaxy = _galaxies[i];
                for (var j = i + 1; j < _galaxies.Count; j++)
                {
                    var endGalaxy = _galaxies[j];
                    var verticalPath = Math.Abs(endGalaxy.I - startGalaxy.I);
                    for (var pathI = startGalaxy.I + 1; pathI <= endGalaxy.I - 1; pathI++)
                    {
                        if (_emptyRows.Contains(pathI))
                        {
                            verticalPath = verticalPath - 1 + expansionDistance;
                        }
                    }

                    var horizontalPath = Math.Abs(endGalaxy.J - startGalaxy.J);
                    var emptyColumnSearchStart = startGalaxy.J > endGalaxy.J ? endGalaxy.J + 1 : startGalaxy.J + 1;
                    var emptyColumnSearchEnd = startGalaxy.J > endGalaxy.J ? startGalaxy.J - 1 : endGalaxy.J - 1;

                    for (var pathJ = emptyColumnSearchStart; pathJ <= emptyColumnSearchEnd; pathJ++)
                    {
                        if (_emptyColumns.Contains(pathJ))
                        {
                            horizontalPath = horizontalPath - 1 + expansionDistance;
                        }
                    }

                    var path = verticalPath + horizontalPath;
                    sum += path;
                }
            }

            return sum;
        }

        private readonly struct Point(int i, int j)
        {
            public int I { get; } = i;

            public int J { get; } = j;
        }
    }
}
