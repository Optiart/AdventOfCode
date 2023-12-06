namespace Tasks.DayTwo
{
    public class Game(int id, IEnumerable<Game.Set> revealedSets)
    {
        public int Id { get; } = id;

        public Set[] RevealedSets { get; } = revealedSets.ToArray<Set>();

        public static Game Parse(string input)
        {
            var parts = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var id = int.Parse(parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            var setParts = parts[1].Split(";", StringSplitOptions.RemoveEmptyEntries);
            var sets = new List<Set>();

            foreach (var setPart in setParts)
            {
                var blueRevealed = 0;
                var redRevealed = 0;
                var greenRevealed = 0;

                var balls = setPart.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var ball in balls)
                {
                    var ballParts = ball.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (ballParts[1].Trim().Equals("blue"))
                    {
                        blueRevealed = int.Parse(ballParts[0]);
                    }
                    else if (ballParts[1].Trim().Equals("green"))
                    {
                        greenRevealed = int.Parse(ballParts[0]);
                    }
                    else if (ballParts[1].Trim().Equals("red"))
                    {
                        redRevealed = int.Parse(ballParts[0]);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unable to parse ball part {ballParts[1]}");
                    }
                }

                sets.Add(new Set(blueRevealed, redRevealed, greenRevealed));
            }

            return new Game(id, sets);
        }

        public class Set(int blueRevealed, int redRevealed, int greenRevealed)
        {
            public int BlueRevealed { get; } = blueRevealed;

            public int RedRevealed { get; } = redRevealed;

            public int GreenRevealed { get; } = greenRevealed;

            public int Sum => BlueRevealed + RedRevealed + GreenRevealed;
        }
    }
}
