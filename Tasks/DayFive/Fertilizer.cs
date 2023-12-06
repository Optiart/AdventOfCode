namespace Tasks.DayFive
{
    public class Fertilizer
    {
        private long[] _seeds = Array.Empty<long>();
        private readonly List<Range> _seedRanges = new();
        private readonly List<List<MapLine>> _maps = new();

        public Fertilizer(string[] inputLines)
        {
            ReadSeeds(inputLines.First());

            var mapByName = new HashSet<string>()
            {
                "seed-to-soil map:",
                "soil-to-fertilizer map:",
                "fertilizer-to-water map:",
                "water-to-light map:",
                "light-to-temperature map:",
                "temperature-to-humidity map:",
                "humidity-to-location map:"
            };

            for (var i = 1; i < inputLines.Length; i++)
            {
                if (mapByName.Contains(inputLines[i]))
                {
                    var map = ParseMap(inputLines, i);
                    _maps.Add(map);
                }
            }
        }

        private void ReadSeeds(string input)
        {
            _seeds = input
                .Split(':', StringSplitOptions.RemoveEmptyEntries).Last()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            for (var i = 0; i < _seeds.Length; i += 2)
            {
                _seedRanges.Add(new Range(_seeds[i], _seeds[i] + _seeds[i + 1] - 1));
            }
        }

        private List<MapLine> ParseMap(string[] inputLines, int i)
        {
            var map = new List<MapLine>();
            var j = i + 1;

            while (j < inputLines.Length && inputLines[j] != string.Empty)
            {
                var mapDetails = inputLines[j]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                var destination = mapDetails[0];
                var source = mapDetails[1];
                var range = mapDetails[2];

                map.Add(new MapLine
                {
                    Source = new Range(source, source + range - 1),
                    Destination = new Range(destination, destination + range - 1)
                });

                j++;
            }

            return map.OrderBy(m => m.Source.Start).ToList();
        }

        public long FindMinimumLocationForSeedRanges()
        {
            long result = long.MaxValue;
            foreach (var seedRange in _seedRanges)
            {
                result = Math.Min(result, FindMinimumLocationForSeedRanges(seedRange));
            }

            return result;
        }

        public long FindMinimumLocation() => _seeds.Min(FindLocation);

        public long FindLocation(long seedNumber)
        {
            var input = seedNumber;
            foreach (var map in _maps)
            {
                var mapped = MapTo(input, map);
                input = mapped;
            }

            return input;
        }

        private long MapTo(long input, List<MapLine> map)
        {
            var result = input;

            if (input < map.First().Source.Start || input > map.Last().Source.End)
            {
                return result;
            }

            foreach (var mapLine in map)
            {
                if (input >= mapLine.Source.Start && input <= mapLine.Source.End)
                {
                    var diff = input - mapLine.Source.Start;
                    result = mapLine.Destination.Start + diff;
                    break;
                }
            }
            return result;
        }

        private long FindMinimumLocationForSeedRanges(Range seedRange)
        {
            var queue = new Queue<(Range range, int currentMap)>();
            queue.Enqueue((seedRange, 0));
            var result = long.MaxValue;

            while (queue.Any())
            {
                var pair = queue.Dequeue();
                var currentMapLines = _maps[pair.currentMap];

                //Console.WriteLine($"Processing map {pair.currentMap} for range {pair.range}");

                var resultRanges = MergeMapLines(pair.range, currentMapLines);

                if (pair.currentMap + 1 == _maps.Count)
                {
                    result = Math.Min(result, resultRanges.Min(r => r.Start));
                    continue;
                }

                var nextMap = ++pair.currentMap;
                foreach (var resultRange in resultRanges)
                {
                    queue.Enqueue((resultRange, nextMap));
                }
            }

            return result;
        }

        public static Range[] MergeMapLines(Range input, MapLine target)
        {
            return MergeMapLines(input, new List<MapLine>() {target});
        }

        public static Range[] MergeMapLines(Range input, List<MapLine> targets)
        {
            var inputSourceStart = input.Start;
            var inputSourceEnd = input.End;

            var result = new List<Range>();
            if (inputSourceEnd < targets.First().Source.Start ||
                inputSourceStart > targets.Last().Source.End)
            {
                result.Add(input);
                return result.ToArray();
            }

            foreach (var target in targets)
            {
                if (inputSourceStart > target.Source.End)
                {
                    continue;
                }

                if (inputSourceStart < target.Source.Start)
                {
                    var leftRange = new Range(inputSourceStart, target.Source.Start - 1);
                    result.Add(leftRange);

                    inputSourceStart = target.Source.Start;
                }

                var intersectStart = inputSourceStart - target.Source.Start;
                var destinationSource = target.Destination.Start + intersectStart;

                var rangeToAdd = Math.Min(inputSourceEnd - inputSourceStart, target.Source.End - inputSourceStart);
                var destinationEnd = destinationSource + rangeToAdd;
                inputSourceStart = target.Source.End + 1;

                if (target.Source.End - inputSourceStart > 0)
                {
                    destinationEnd = target.Destination.End;
                }

                var newRange = new Range(destinationSource, destinationEnd);
                result.Add(newRange);

                if (inputSourceEnd - inputSourceStart <= 0)
                {
                    break;
                }
            }

            if (inputSourceEnd - inputSourceStart > 0)
            {
                var lastRange = new Range(inputSourceStart, inputSourceEnd);
                result.Add(lastRange);
            }

            return result.OrderBy(r => r.Start).ToArray();
        }

        public struct MapLine
        {
            public Range Source { get; set; }

            public Range Destination { get; set; }

            public MapLine()
            {
                
            }

            public MapLine(Range source, Range destination)
            {
                Source = source;
                Destination = destination;
            }

            public override string ToString() => $"Source({Source.Start}-{Source.End}); Destination({Destination.Start}-{Destination.End})";
        }

        public struct Range
        {
            public long Start { get; set; }

            public long End { get; set; }

            public Range()
            {
                
            }

            public Range(long start, long end)
            {
                Start = start;
                End = end;
            }

            public override string ToString() => $"Start: {Start}; End: {End}";
        }
    }
}
