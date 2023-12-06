using FluentAssertions;
using Tasks.DayTwo;

namespace Tests
{
    public class DayTwoTests
    {
        [Fact]
        public void ParsesInput()
        {
            // Arrange
            var input = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";

            // Act
            var game = Game.Parse(input);

            var expectedGame = new Game(3, new[]
            {
                new Game.Set(blueRevealed: 6, redRevealed: 20, greenRevealed: 8),
                new Game.Set(blueRevealed: 5, redRevealed: 4, greenRevealed: 13),
                new Game.Set(blueRevealed: 0, redRevealed: 1, greenRevealed: 5)
            });

            // Assert
            game.Should().BeEquivalentTo(expectedGame);
        }


        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true)]
        [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", true)]
        [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", false)]
        [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", false)]
        [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", true)]
        public void DeterminesPossibilityOfAGameFromExample(string gameInput, bool isPossible)
        {
            // Arrange
            var game = Game.Parse(gameInput);
            var conundrum = new CubeConundrum();

            // Act
            var result = conundrum.IsGamePossible(game);

            // Assert
            result.Should().Be(isPossible);
        }

        [Fact]
        public void CalculatesSumOfPossibleGamesFromExample()
        {
            // Arrange
            var input = new[]
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };

            var conundrum = new CubeConundrum(input);

            // Act
            var result = conundrum.CalculateSumOfPossibleGameIds();

            // Assert
            result.Should().Be(8);
        }

        [Theory]
        [InlineData("Game 13: 1 blue, 16 green, 1 red; 6 red, 2 blue, 5 green; 2 blue, 12 red, 10 green; 3 red, 4 blue, 13 green; 14 red, 4 blue, 12 green; 7 red, 2 green", false)]
        [InlineData("Game 37: 8 red, 7 green; 5 green, 1 blue, 6 red; 7 red, 6 blue, 11 green", true)]
        [InlineData("Game 54: 3 blue, 3 green, 18 red; 4 blue, 18 red, 3 green; 7 blue, 4 green", false)]
        [InlineData("Game 62: 7 red, 6 blue, 8 green; 10 blue, 3 green, 17 red; 13 blue, 3 red, 10 green; 13 red, 5 blue, 9 green; 12 blue, 4 red; 10 red, 4 green", false)]
        [InlineData("Game 95: 4 blue, 13 red; 5 blue, 1 green, 11 red; 3 green, 3 blue, 10 red; 13 red, 6 blue; 2 green, 5 blue; 3 green, 11 red", false)]
        [InlineData("Game 10: 8 red, 3 blue, 5 green; 5 green, 7 blue, 1 red; 3 red, 10 blue, 6 green; 2 red, 6 green, 7 blue; 3 blue, 11 red, 4 green; 8 red, 8 blue, 4 green", true)]
        public void DeterminesPossibilityOfAGameFromInput(string gameInput, bool isPossible)
        {
            // Arrange
            var game = Game.Parse(gameInput);
            var conundrum = new CubeConundrum();

            // Act
            var result = conundrum.IsGamePossible(game);

            // Assert
            result.Should().Be(isPossible);
        }

        [Fact]
        public void CalculatesSumOfPossibleGamesFromInput()
        {
            // Arrange
            var input = new[]
            {
                "Game 13: 1 blue, 16 green, 1 red; 6 red, 2 blue, 5 green; 2 blue, 12 red, 10 green; 3 red, 4 blue, 13 green; 14 red, 4 blue, 12 green; 7 red, 2 green",
                "Game 37: 8 red, 7 green; 5 green, 1 blue, 6 red; 7 red, 6 blue, 11 green",
                "Game 54: 3 blue, 3 green, 18 red; 4 blue, 18 red, 3 green; 7 blue, 4 green",
                "Game 62: 7 red, 6 blue, 8 green; 10 blue, 3 green, 17 red; 13 blue, 3 red, 10 green; 13 red, 5 blue, 9 green; 12 blue, 4 red; 10 red, 4 green",
                "Game 95: 4 blue, 13 red; 5 blue, 1 green, 11 red; 3 green, 3 blue, 10 red; 13 red, 6 blue; 2 green, 5 blue; 3 green, 11 red",
                "Game 10: 8 red, 3 blue, 5 green; 5 green, 7 blue, 1 red; 3 red, 10 blue, 6 green; 2 red, 6 green, 7 blue; 3 blue, 11 red, 4 green; 8 red, 8 blue, 4 green"
            };

            var conundrum = new CubeConundrum(input);

            // Act
            var result = conundrum.CalculateSumOfPossibleGameIds();

            // Assert
            result.Should().Be(47);
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
        [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]
        [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560)]
        [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630)]
        [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36)]
        public void CalculatesPowerOfFewestNumberOfCubesNeededFromExample(string gameInput, int expectedPower)
        {
            // Arrange
            var game = Game.Parse(gameInput);
            var conundrum = new CubeConundrum();

            // Act
            var result = conundrum.CalculatePowerOfFewestNumberOfCubesNeeded(game);

            // Assert
            result.Should().Be(expectedPower);
        }

        [Fact]
        public void CalculatesSumOfPowerOfFewestNumberOfCubesNeededFromExample()
        {
            // Arrange
            var input = new[]
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };

            var conundrum = new CubeConundrum(input);

            // Act
            var result = conundrum.CalculateSumOfPowerOfFewestNumberOfCubesNeeded();

            // Assert
            result.Should().Be(2286);
        }

        [Theory]
        [InlineData("Game 13: 1 blue, 16 green, 1 red; 6 red, 2 blue, 5 green; 2 blue, 12 red, 10 green; 3 red, 4 blue, 13 green; 14 red, 4 blue, 12 green; 7 red, 2 green", 896)]
        [InlineData("Game 37: 8 red, 7 green; 5 green, 1 blue, 6 red; 7 red, 6 blue, 11 green", 528)]
        [InlineData("Game 54: 3 blue, 3 green, 18 red; 4 blue, 18 red, 3 green; 7 blue, 4 green", 504)]
        [InlineData("Game 62: 7 red, 6 blue, 8 green; 10 blue, 3 green, 17 red; 13 blue, 3 red, 10 green; 13 red, 5 blue, 9 green; 12 blue, 4 red; 10 red, 4 green", 2210)]
        [InlineData("Game 95: 4 blue, 13 red; 5 blue, 1 green, 11 red; 3 green, 3 blue, 10 red; 13 red, 6 blue; 2 green, 5 blue; 3 green, 11 red", 234)]
        [InlineData("Game 10: 8 red, 3 blue, 5 green; 5 green, 7 blue, 1 red; 3 red, 10 blue, 6 green; 2 red, 6 green, 7 blue; 3 blue, 11 red, 4 green; 8 red, 8 blue, 4 green", 660)]
        public void CalculatesPowerOfFewestNumberOfCubesNeededFromInput(string gameInput, int expectedPower)
        {
            // Arrange
            var game = Game.Parse(gameInput);
            var conundrum = new CubeConundrum();

            // Act
            var result = conundrum.CalculatePowerOfFewestNumberOfCubesNeeded(game);

            // Assert
            result.Should().Be(expectedPower);
        }

        [Fact]
        public void CalculatesSumOfPowerOfFewestNumberOfCubesNeededFromInput()
        {
            // Arrange
            var input = new[]
            {
                "Game 13: 1 blue, 16 green, 1 red; 6 red, 2 blue, 5 green; 2 blue, 12 red, 10 green; 3 red, 4 blue, 13 green; 14 red, 4 blue, 12 green; 7 red, 2 green",
                "Game 37: 8 red, 7 green; 5 green, 1 blue, 6 red; 7 red, 6 blue, 11 green",
                "Game 54: 3 blue, 3 green, 18 red; 4 blue, 18 red, 3 green; 7 blue, 4 green",
                "Game 62: 7 red, 6 blue, 8 green; 10 blue, 3 green, 17 red; 13 blue, 3 red, 10 green; 13 red, 5 blue, 9 green; 12 blue, 4 red; 10 red, 4 green",
                "Game 95: 4 blue, 13 red; 5 blue, 1 green, 11 red; 3 green, 3 blue, 10 red; 13 red, 6 blue; 2 green, 5 blue; 3 green, 11 red",
                "Game 10: 8 red, 3 blue, 5 green; 5 green, 7 blue, 1 red; 3 red, 10 blue, 6 green; 2 red, 6 green, 7 blue; 3 blue, 11 red, 4 green; 8 red, 8 blue, 4 green"
            };

            var conundrum = new CubeConundrum(input);

            // Act
            var result = conundrum.CalculateSumOfPowerOfFewestNumberOfCubesNeeded();

            // Assert
            result.Should().Be(5032);
        }
    }
}
