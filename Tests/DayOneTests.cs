using FluentAssertions;

namespace Tests
{
    public class DayOneTests
    {
        [Theory]
        [InlineData("1abc2", 12)]
        [InlineData("pqr3stu8vwx", 38)]
        [InlineData("a1b2c3d4e5f", 15)]
        [InlineData("treb7uchet", 77)]
        public void CalculatesCalibrationValueFromExample(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.GetCalibrationValue(input);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("hs1", 11)]
        [InlineData("742", 72)]
        [InlineData("9fivernlsl6jvfrljzjg", 96)]
        [InlineData("nn88", 88)]
        [InlineData("eightdgkcrphqsndn7fivevkstrsprktqdrdxqslmjtz", 77)]
        [InlineData("35", 35)]
        [InlineData("9", 99)]
        public void CalculatesCalibrationValueFromInput(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.GetCalibrationValue(input);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Fact]
        public void CalculatesSumOfCalibrationValues()
        {
            // Arrange
            var inputValues = new[]
            {
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            };

            // Act
            var result = Trebuchet.GetSumOfCalibrationValues(inputValues, includeStringNumbers: false);

            // Assert
            result.Should().Be(142);
        }

        [Theory]
        [InlineData("two", 2)]
        [InlineData("xtwo", 2)]
        [InlineData("xasdsadasdytwo", 2)]
        [InlineData("xyztwotes", 2)]
        [InlineData("xyztwthreeotes", 3)]
        [InlineData("xyztwthrenineotes", 9)]
        public void FindNumberInStringForward(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.FindNumberInWord(input, isForward: true);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("owt", 2)]
        [InlineData("xowt", 2)]
        [InlineData("xasdsadasdytwowt", 2)]
        [InlineData("xyztwowttes", 2)]
        [InlineData("xyztwthreeeerhtotes", 3)]
        [InlineData("xyztwthrenineeninotes", 9)]
        public void FindNumberInStringBackward(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.FindNumberInWord(input, isForward: false);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("two1nine", 29)]
        [InlineData("eightwothree", 83)]
        [InlineData("abcone2threexyz", 13)]
        [InlineData("xtwone3four", 24)]
        [InlineData("4nineeightseven2", 42)]
        [InlineData("zoneight234", 14)]
        [InlineData("7pqrstsixteen", 76)]
        [InlineData("oneight", 18)]
        public void CalculatesCalibrationValueIncludingStringNumbersFromExample(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.GetCalibrationValueIncludingString(input);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("8nineninesevenvxfnqlsrnxbr", 87)]
        [InlineData("2bk", 22)]
        [InlineData("fivetnxmljplldd3six", 56)]
        [InlineData("pxkqbgdfour89", 49)]
        [InlineData("nn88", 88)]
        [InlineData("fivejtxmg4hbctmpnntlpbzfmxqgthreejbjdb", 53)]
        [InlineData("3", 33)]
        [InlineData("three", 33)]
        public void CalculatesCalibrationValueIncludingStringNumbersFromInput(string input, int expectedValue)
        {
            // Act
            var result = Trebuchet.GetCalibrationValueIncludingString(input);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Fact]
        public void CalculatesSumOfCalibrationValuesIncludingStringNumbers()
        {
            // Arrange
            var inputValues = new[]
            {
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen"
            };

            // Act
            var result = Trebuchet.GetSumOfCalibrationValues(inputValues, includeStringNumbers: true);

            // Assert
            result.Should().Be(281);
        }
    }
}