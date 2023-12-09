using FluentAssertions;
using Tasks.DayNine;

namespace Tests
{
    public class DayNineTests
    {
        [Theory]
        [InlineData("0 3 6 9 12 15", 18)]
        [InlineData("1 3 6 10 15 21", 28)]
        [InlineData("10 13 16 21 30 45", 68)]
        public void PredictsNextValue(string inputLine, int expectedResult)
        {
            // Arrange
            var inputLines = new[] { inputLine };
            var sut = new MirageMaintenance(inputLines);

            // Act
            var result = sut.CalculateSumOfNextPredictedValues();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CalculatesSumOfPredictedNextValues()
        {
            // Arrange
            var inputLines = new[]
            {
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45"
            };
            var sut = new MirageMaintenance(inputLines);

            // Act
            var result = sut.CalculateSumOfNextPredictedValues();

            // Assert
            result.Should().Be(114);
        }

        [Theory]
        [InlineData("0 3 6 9 12 15", -3)]
        [InlineData("1 3 6 10 15 21", 0)]
        [InlineData("10 13 16 21 30 45", 5)]
        public void PredictsPreviousValue(string inputLine, int expectedResult)
        {
            // Arrange
            var inputLines = new[] { inputLine };
            var sut = new MirageMaintenance(inputLines);

            // Act
            var result = sut.CalculateSumOfPreviousPredictedValues();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CalculatesSumOfPredictedPreviousValues()
        {
            // Arrange
            var inputLines = new[]
            {
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45"
            };
            var sut = new MirageMaintenance(inputLines);

            // Act
            var result = sut.CalculateSumOfPreviousPredictedValues();

            // Assert
            result.Should().Be(2);
        }
    }
}
