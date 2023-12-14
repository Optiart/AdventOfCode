using FluentAssertions;
using Tasks.DayTwelve;

namespace Tests
{
    public class DayTwelveTests
    {
        [Theory]
        [InlineData("? 1", 1)]
        [InlineData("?? 1", 2)]

        [InlineData("# 1", 1)]
        [InlineData(". 0", 0)]

        [InlineData("..? 1", 1)]
        [InlineData(".?. 1", 1)]
        [InlineData("?.. 1", 1)]

        [InlineData(".?? 1", 2)]
        [InlineData("??. 1", 2)]
        [InlineData("??? 1", 3)]

        [InlineData("?#? 1", 1)]
        [InlineData("??# 1", 1)]
        [InlineData("#?? 1", 1)]
        public void CalculatesArrangementsSimple(string inputLine, int expectedCount)
        {
            // Arrange
            var inputLines = new[] { inputLine };
            var sut = new HotSprings(inputLines);

            // Act
            var result = sut.CalculateSumOfArrangements();

            // Assert
            result.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData("#.#.### 1,1,3", 1)]
        [InlineData(".#...#....###. 1,1,3", 1)]
        [InlineData(".#.###.#.###### 1,3,1,6", 1)]
        [InlineData("####.#...#... 4,1,1", 1)]
        [InlineData("#....######..#####. 1,6,5", 1)]
        [InlineData(".###.##....# 3,2,1", 1)]
        public void CalculatesArrangementsWithNoUnknowns(string inputLine, int expectedCount)
        {
            // Arrange
            var inputLines = new[] { inputLine };
            var sut = new HotSprings(inputLines);

            // Act
            var result = sut.CalculateSumOfArrangements();

            // Assert
            result.Should().Be(expectedCount);
        }

        [Theory]
        [InlineData("???.### 1,1,3", 1)]
        [InlineData(".??..??...?##. 1,1,3", 4)]
        [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
        [InlineData("????.#...#... 4,1,1", 1)]
        [InlineData("????.######..#####. 1,6,5", 4)]
        [InlineData("?###???????? 3,2,1", 10)]
        public void CalculatesArrangementsWithUnknowns(string inputLine, int expectedCount)
        {
            // Arrange
            var inputLines = new[] { inputLine };
            var sut = new HotSprings(inputLines);

            // Act
            var result = sut.CalculateSumOfArrangements();

            // Assert
            result.Should().Be(expectedCount);
        }
    }
}
