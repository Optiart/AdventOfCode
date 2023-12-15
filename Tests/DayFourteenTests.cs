using FluentAssertions;
using Tasks.DayFourteen;

namespace Tests
{
    public class DayFourteenTests
    {
        [Fact]
        public void CalculatesWithNoBlockers()
        {
            // Arrange
            var input =
                """
                O....
                .O...
                ..O..
                ...O.
                ....O
                """;

            var inputLines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new ParabolicReflectorDish(inputLines);

            // Act
            var result = sut.CalculateNorthLoad();

            // Assert
            result.Should().Be(25);
        }

        [Fact]
        public void CalculatesWithRockBlockers()
        {
            // Arrange
            var input =
                """
                O#...
                .O#..
                ..O#.
                ...O#
                ....O
                """;

            var inputLines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new ParabolicReflectorDish(inputLines);

            // Act
            var result = sut.CalculateNorthLoad();

            // Assert
            result.Should().Be(15);
        }

        [Fact]
        public void CalculatesWithRoundRockBlockers()
        {
            // Arrange
            var input =
                """
                OO...
                .OO..
                ..OO.
                ...OO
                ....O
                """;

            var inputLines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new ParabolicReflectorDish(inputLines);

            // Act
            var result = sut.CalculateNorthLoad();

            // Assert
            result.Should().Be(41);
        }

        [Fact]
        public void CalculatesExampleInput()
        {
            // Arrange
            var input =
                """
                O....#....
                O.OO#....#
                .....##...
                OO.#O....O
                .O.....O#.
                O.#..O.#.#
                ..O..#O..O
                .......O..
                #....###..
                #OO..#....
                """;

            var inputLines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new ParabolicReflectorDish(inputLines);

            // Act
            var result = sut.CalculateNorthLoad();

            // Assert
            result.Should().Be(136);
        }
    }
}
