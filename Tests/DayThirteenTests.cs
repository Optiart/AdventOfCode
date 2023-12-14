using FluentAssertions;
using Tasks.DayThirteen;

namespace Tests
{
    public class DayThirteenTests
    {
        [Fact]
        public void CalculatesReflectionsForExampleVertical_ShiftedRight()
        {
            // Arrange
            var pattern = """
                          #.##..##.
                          ..#.##.#.
                          ##......#
                          ##......#
                          ..#.##.#.
                          ..##..##.
                          #.#.##.#.
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void CalculatesReflectionsForExampleVertical_ShiftedLeft()
        {
            // Arrange
            var pattern = """
                          .##..##.#
                          .#.##.#..
                          #......##
                          #......##
                          .#.##.#..
                          .##..##..
                          .#.##.#.#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(4);
        }

        [Fact]
        public void CalculatesReflectionsForExampleVertical_Centered()
        {
            // Arrange
            var pattern = """
                          #.##..##.#
                          ..#.##.#..
                          ##......##
                          ##......##
                          ..#.##.#..
                          ..##..##..
                          #.#.##.#.#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void CalculatesReflectionsForExampleHorizontal_ShiftedBottom()
        {
            // Arrange
            var pattern = """
                          #...##..#
                          #....#..#
                          ..##..###
                          #####.##.
                          #####.##.
                          ..##..###
                          #....#..#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(400);
        }

        [Fact]
        public void CalculatesReflectionsForExampleHorizontal_ShiftedTop()
        {
            // Arrange
            var pattern = """
                          #....#..#
                          ..##..###
                          #####.##.
                          #####.##.
                          ..##..###
                          #....#..#
                          #...##..#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(300);
        }

        [Fact]
        public void CalculatesReflectionsForExampleHorizontal_Centered()
        {
            // Arrange
            var pattern = """
                          #...##..#
                          #....#..#
                          ..##..###
                          #####.##.
                          #####.##.
                          ..##..###
                          #....#..#
                          #...##..#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections();

            // Assert
            result.Should().Be(400);
        }

        [Fact]
        public void CalculatesReflectionsForExampleVertical_ShiftedRight_WithSmudge()
        {
            // Arrange
            var pattern = """
                          #.##..##.
                          ..#.##.#.
                          ##......#
                          ##......#
                          ..#.##.#.
                          ..##..##.
                          #.#.##.#.
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections(hasSmudge: true);

            // Assert
            result.Should().Be(300);
        }

        [Fact]
        public void CalculatesReflectionsForExampleHorizontal_ShiftedBottom_WithSmudge()
        {
            // Arrange
            var pattern = """
                          #...##..#
                          #....#..#
                          ..##..###
                          #####.##.
                          #####.##.
                          ..##..###
                          #....#..#
                          """;
            var inputLines = pattern.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var sut = new PointOfIncidence(inputLines);

            // Act
            var result = sut.CalculateReflections(hasSmudge: true);

            // Assert
            result.Should().Be(100);
        }
    }
}
