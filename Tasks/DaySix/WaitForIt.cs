namespace Tasks.DaySix
{
    public class WaitForIt
    {
        private readonly List<Race> _races = new();

        public WaitForIt(string[] inputLines, bool hasKerning = false)
        {
            var timeParts = GetParts(hasKerning ? inputLines[0].Replace(" ", string.Empty) : inputLines[0]);
            var currentRecordParts = GetParts(hasKerning ? inputLines[1].Replace(" ", string.Empty) : inputLines[1]);

            for (var i = 0; i < timeParts.Length; i++)
            {
                if (!long.TryParse(timeParts[i], out var timeValue))
                {
                    continue;
                }

                _races.Add(new Race()
                {
                    Time = timeValue,
                    CurrentRecord = long.Parse(currentRecordParts[i])
                });
            }
        }

        private int CalculateWaysToWinOptimizedIterations(Race race)
        {
            int i;

            for (i = 1; i <= race.Time - 1; i++)
            {
                var travelTime = race.Time - i;
                var distance = travelTime * i;

                if (distance > race.CurrentRecord)
                {
                    break;
                }
            }

            return (int)race.Time - i * 2 + 1;
        }

        private int CalculateWaysToWinIterations(Race race)
        {
            var currentWins = 0;

            for (var i = 1; i <= race.Time - 1; i++)
            {
                var travelTime = race.Time - i;
                var distance = travelTime * i;

                if (distance > race.CurrentRecord)
                {
                    currentWins++;
                }
            }

            return currentWins;
        }

        public int CalculateWaysToWinWithMath()
        {
            var result = 1;
            foreach (var race in _races)
            {
                var (lowerBoundaryToWin, upperBoundaryToWin) = CalculateTimeTaken(race.Time, race.CurrentRecord);
                var raceResult = upperBoundaryToWin - lowerBoundaryToWin + 1;

                result *= raceResult;
            }

            return result;
        }

        private (int lowerBoundaryToWin, int upperBoundaryToWin) CalculateTimeTaken(long raceTime, long distance)
        {
            // equation is button_hold^2 - race_time*button_hold + distance = 0
            double rootOne, rootTwo;
            double d = raceTime * raceTime - 4 * distance;

            rootOne = d >= 0 ? (raceTime + Math.Sqrt(d)) / 2 : raceTime / 2;
            rootTwo = d >= 0 ? (raceTime - Math.Sqrt(d)) / 2 : Math.Sqrt(-d) / 2;

            var lowerBoundary = Math.Min(rootOne, rootTwo);
            var upperBoundary = Math.Max(rootOne, rootTwo);

            var roundedLowerBoundary = (int)Math.Round(lowerBoundary, MidpointRounding.ToPositiveInfinity);
            var roundedUpperBoundary = (int)Math.Round(upperBoundary, MidpointRounding.ToNegativeInfinity);

            var lowerBoundaryToWin = HasReminder(lowerBoundary) ? roundedLowerBoundary : roundedLowerBoundary + 1;
            var upperBoundaryToWin = HasReminder(upperBoundary) ? roundedUpperBoundary : roundedUpperBoundary - 1;

            return (lowerBoundaryToWin, upperBoundaryToWin);

            bool HasReminder(double value) => value % 1 != 0;
        }

        public int CalculateWaysToWin()
        {
            var result = 1;
            foreach (var race in _races)
            {
                result *= CalculateWaysToWinOptimizedIterations(race);
            }

            return result;
        }

        public int CalculateWaysToWinIterations()
        {
            var result = 1;
            foreach (var race in _races)
            {
                result *= CalculateWaysToWinIterations(race);
            }

            return result;
        }

        private string[] GetParts(string input) => 
            input.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        public class Race
        {
            public long Time { get; set; }

            public long CurrentRecord { get; set; }
        }
    }
}
