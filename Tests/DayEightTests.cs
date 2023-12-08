using FluentAssertions;
using Tasks.DayEight;

namespace Tests
{
    public class DayEightTests
    {
        [Fact]
        public void FindPath()
        {
            // Arrange
            var inputLines = new[]
            {
                "RL",
                "",
                "AAA = (BBB, CCC)",
                "BBB = (DDD, EEE)",
                "CCC = (ZZZ, GGG)",
                "DDD = (DDD, DDD)",
                "EEE = (EEE, EEE)",
                "GGG = (GGG, GGG)",
                "ZZZ = (ZZZ, ZZZ)"
            };

            var sut = new HauntedWasteland(inputLines);

            // Act
            var result = sut.FindStepsToDestination();

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void FindsPathInCycle()
        {
            // Arrange
            var inputLines = new[]
            {
                "LLR",
                "",
                "AAA = (BBB, BBB)",
                "BBB = (AAA, ZZZ)",
                "ZZZ = (ZZZ, ZZZ)"
            };

            var sut = new HauntedWasteland(inputLines);

            // Act
            var result = sut.FindStepsToDestination();

            // Assert
            result.Should().Be(6);
        }

        [Fact]
        public void FindsPathForGhosts()
        {
            // Arrange
            var inputLines = new[]
            {
                "LR",
                "",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)"
            };

            var sut = new HauntedWasteland(inputLines);

            // Act
            var traverseResult = sut.FindStepsToDestinationAsGhost();
            var lcm = sut.FindStepsToDestinationAsGhostViaLcm();

            // Assert
            lcm.Should().Be(traverseResult);
        }

        [Fact]
        public void FindsEasyPathForGhosts()
        {
            // Arrange
            var inputLines = new[]
            {
                "R",
                "",
                "11A = (22B, XXZ)",
                "22A = (22B, XXZ)",
                "22B = (22B, 22B)",
                "XXZ = (XXZ, XXZ)"
            };

            var sut = new HauntedWasteland(inputLines);

            // Act
            var traverseResult = sut.FindStepsToDestinationAsGhost();
            var lcm = sut.FindStepsToDestinationAsGhostViaLcm();

            // Assert
            lcm.Should().Be(traverseResult);
        }
    }
}
