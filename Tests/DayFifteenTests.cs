using FluentAssertions;
using Tasks.DayFifteen;

namespace Tests
{
    public class DayFifteenTests
    {
        [Theory]
        [InlineData("HASH", 52)]
        [InlineData("rn=1", 30)]
        [InlineData("rn=1,cm-", 283)]
        [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", 1320)]
        public void ComputesInitializationSequenceSum(string str, int expectedResult)
        {
            // Arrange
            var inputLines = new[] { str };
            var sut = new LensLibrary(inputLines);

            // Act
            var result = sut.ComputeInitializationSequenceSum();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("rn=1", 1)]
        [InlineData("rn=1,cm-", 1)]
        [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", 145)]
        public void ComputesFocusingPower(string str, int expectedResult)
        {
            // Arrange
            var inputLines = new[] { str };
            var sut = new LensLibrary(inputLines);

            // Act
            var result = sut.ComputeFocusingPower();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
