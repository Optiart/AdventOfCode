namespace Tasks.DayTwo
{
    public class CubeConundrum()
    {
        public int MaxGreenCount { get; set; } = 13;

        public int MaxBlueCount { get; set; } = 14;

        public int MaxRedCount { get; set; } = 12;

        public int MaxCount => MaxGreenCount + MaxBlueCount + MaxRedCount;

        private readonly Game[] _games;

        public CubeConundrum(string[] inputLines) : this() => 
            _games = inputLines.Select(Game.Parse).ToArray();

        public bool IsGamePossible(Game game) =>
            game.RevealedSets.All(s =>
                s.BlueRevealed <= MaxBlueCount &&
                s.GreenRevealed <= MaxGreenCount &&
                s.RedRevealed <= MaxRedCount &&
                s.Sum <= MaxCount);

        public int CalculateSumOfPossibleGameIds() =>
            _games.Where(IsGamePossible).Sum(g => g.Id);

        public int CalculatePowerOfFewestNumberOfCubesNeeded(Game game) =>
                game.RevealedSets.Max(rs => rs.RedRevealed) *
                game.RevealedSets.Max(rs => rs.BlueRevealed) *
                game.RevealedSets.Max(rs => rs.GreenRevealed);

        public int CalculateSumOfPowerOfFewestNumberOfCubesNeeded() =>
            _games.Sum(CalculatePowerOfFewestNumberOfCubesNeeded);
    }
}
