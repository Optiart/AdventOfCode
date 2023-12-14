using Tasks.Helpers;

namespace Tasks.DayThirteen
{
    public class PointOfIncidence
    {
        private readonly List<Pattern> _patterns = new();

        public PointOfIncidence(string[] inputLines)
        {
            for (var i = 0; i < inputLines.Length; i++)
            {
                var pattern = new Pattern();
                while (i < inputLines.Length && !string.IsNullOrEmpty(inputLines[i]))
                {
                    pattern.Map.Add(inputLines[i].ToCharArray());
                    i++;
                }
                _patterns.Add(pattern);
            }
        }

        public long CalculateReflections(bool hasSmudge = false) => _patterns.Sum(p => CalculateReflection(p, hasSmudge));

        private long CalculateReflection(Pattern pattern, bool hasSmudge)
        {
            var verticallyReflectedCount = GetVerticalReflectedCount(pattern.Map, hasSmudge);

            if (verticallyReflectedCount > 0)
            {
                return verticallyReflectedCount;
            }

            var transposedMap = pattern.GetTransposedMap();
            var horizontallyReflectedCount = GetVerticalReflectedCount(transposedMap, hasSmudge);

            if (horizontallyReflectedCount > 0)
            {
                return horizontallyReflectedCount * 100;
            }

            throw new Exception("Solution not found");
        }

        private int GetVerticalReflectedCount(List<char[]> map, bool hasSmudge)
        {
            var length = map[0].Length;
            for (var reflectionLine = 0; reflectionLine < length; reflectionLine++)
            {
                var columnLeft = reflectionLine;
                var columnRight = reflectionLine + 1;
                var differences = 0;

                while (columnLeft >= 0 && columnRight < length)
                {
                    differences += GetVerticalDifference(columnLeft, columnRight, map);

                    if ((!hasSmudge && differences > 0) || (hasSmudge && differences > 1))
                    {
                        break;
                    }

                    if (!CanExpandMore(columnLeft, columnRight))
                    {
                        if (!hasSmudge && differences == 0)
                        {
                            return reflectionLine + 1;
                        }

                        if (hasSmudge && differences == 1)
                        {
                            return reflectionLine + 1;
                        }
                    }

                    columnLeft--;
                    columnRight++;
                }
            }

            bool CanExpandMore(int l, int r) => l - 1 >= 0 && r + 1 < length;

            return 0;
        }

        private int GetVerticalDifference(int i, int j, List<char[]> map)
        {
            var differences = 0;
            for (var r = 0; r < map.Count; r++)
            {
                if (map[r][i] != map[r][j])
                {
                    differences++;
                }
            }

            return differences;
        }

        private class Pattern
        {
            public List<char[]> Map { get; } = new();

            public List<char[]> GetTransposedMap()
            {
                var transposed = new List<char[]>(Map[0].Length);

                for (int j = 0; j < Map[0].Length; j++)
                {
                    transposed.Add(new char[Map.Count]);
                    for (int i = 0; i < Map.Count; i++)
                    {
                        transposed[j][i] = Map[i][j];
                    }
                }

                return transposed;
            }
        }
    }
}
