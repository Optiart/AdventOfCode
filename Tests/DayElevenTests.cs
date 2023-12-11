using FluentAssertions;
using Tasks.DayEleven;

namespace Tests
{
    public class DayElevenTests
    {
        [Fact]
        public void FindsShortestPathSum_MapOne()
        {
            // Arrange
            var inputLines = new[]
            {
                "...#.",
                "....#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum();

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void FindsShortestPathSum_MapTwo()
        {
            // Arrange
            var inputLines = new[]
            {
                "...#.",
                "..#..",
                "....#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum();

            // Assert
            var expectedSum = 2 + 3 + 3;
            result.Should().Be(expectedSum);
        }

        [Theory]
        [InlineData(2, 4)]
        [InlineData(1000000, 1000002)]
        public void FindsShortestPathSum_MapThree_EmptyRow(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#.",
                ".....",
                "....#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 8)]
        [InlineData(1000000, 3000002)]
        public void FindsShortestPathSum_MapFour_EmptyRows(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#.",
                ".....",
                ".....",
                ".....",
                "....#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 4)]
        [InlineData(1000000, 1000002)]
        public void FindsShortestPathSum_MapFive_EmptyRightColumn(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#..",
                ".....#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 10)]
        [InlineData(1000000, 4000002)]
        public void FindsShortestPathSum_MapSix_EmptyRowsAndColumns(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#...",
                ".......",
                ".......",
                "......#",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 374)]
        [InlineData(10, 1030)]
        [InlineData(100, 8410)]
        public void FindsShortestPathSum_MapSeven_Example(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 4)]
        [InlineData(1000000, 1000002)]
        public void FindsShortestPathSum_MapEight_EmptyLeftColumn(int expansionDistance, int expectedResult)
        {
            // Arrange
            var inputLines = new[]
            {
                "...#..",
                ".#....",
            };

            var sut = new CosmicExpansion(inputLines);

            // Act
            var result = sut.FindShortestPathSum(expansionDistance);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
