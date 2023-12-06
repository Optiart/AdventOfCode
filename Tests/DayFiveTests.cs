using FluentAssertions;
using Tasks.DayFive;

namespace Tests
{
    public class DayFiveTests
    {
        [Fact]
        public void MergeRangesWhenInputIsWithinTarget()
        {
            // Arrange
            var input = new Fertilizer.Range(79, 92);

            var target = new Fertilizer.MapLine
            {
                Source = new Fertilizer.Range(50, 97),
                Destination = new Fertilizer.Range(52, 99)
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, target);

            // Assert
            result.Length.Should().Be(1);
            result[0].Start.Should().Be(81); // 79 - 50 = 29; 52 + 29
            result[0].End.Should().Be(94); // 52 + 29 + 14 
        }

        [Fact]
        public void MergeRangesWhenInputIsPartiallyWithinTargetToTheLeft()
        {
            // Arrange
            var input = new Fertilizer.Range(40, 60);

            var target = new Fertilizer.MapLine
            {
                Source = new Fertilizer.Range(50, 97),
                Destination = new Fertilizer.Range(52, 99)
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, target);

            // Assert
            result.Length.Should().Be(2);

            result[0].Start.Should().Be(40);
            result[0].End.Should().Be(49);

            result[1].Start.Should().Be(52);
            result[1].End.Should().Be(62);
        }

        [Fact]
        public void MergeRangesWhenInputIsPartiallyWithinTargetToTheLeftAndRight()
        {
            // Arrange
            var input = new Fertilizer.Range(40, 100);

            var target = new Fertilizer.MapLine
            {
                Source = new Fertilizer.Range(50, 97),
                Destination = new Fertilizer.Range(52, 99)
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, target);

            // Assert
            result.Length.Should().Be(3);

            result[0].Start.Should().Be(40);
            result[0].End.Should().Be(49);

            result[1].Start.Should().Be(52);
            result[1].End.Should().Be(99);

            result[2].Start.Should().Be(98);
            result[2].End.Should().Be(100);
        }

        [Fact]
        public void MergeRangesWhenInputIsPartiallyWithinTargetToTheRight()
        {
            // Arrange
            var input = new Fertilizer.Range(70, 100);

            var target = new Fertilizer.MapLine
            {
                Source = new Fertilizer.Range(50, 97),
                Destination = new Fertilizer.Range(52, 99)
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, target);

            // Assert
            result.Length.Should().Be(2);

            result[0].Start.Should().Be(72);
            result[0].End.Should().Be(99);

            result[1].Start.Should().Be(98);
            result[1].End.Should().Be(100);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(100, 110)]
        public void MergeRangesWhenInputIsOutsideOfTarget(int start, int end)
        {
            // Arrange
            var input = new Fertilizer.Range(start, end);

            var target = new Fertilizer.MapLine
            {
                Source = new Fertilizer.Range(50, 97),
                Destination = new Fertilizer.Range(52, 99)
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, target);

            // Assert
            result.Length.Should().Be(1);
            result[0].Should().Be(input);
        }

        [Fact]
        public void MergeRangesWhenInputIsInTheMiddleOfTheMap()
        {
            // Arrange
            var input = new Fertilizer.Range(70, 100);

            var targets = new List<Fertilizer.MapLine>
            {
                new Fertilizer.MapLine
                {
                    Source = new Fertilizer.Range(13, 40),
                    Destination = new Fertilizer.Range(0, 27)
                },
                new Fertilizer.MapLine
                {
                    Source = new Fertilizer.Range(42, 78),
                    Destination = new Fertilizer.Range(11, 47)
                },
                new Fertilizer.MapLine
                {
                    Source = new Fertilizer.Range(83, 110),
                    Destination = new Fertilizer.Range(11, 47)
                }
            };

            // Act
            var result = Fertilizer.MergeMapLines(input, targets);

            // Assert
            result.Length.Should().Be(3);

            result[0].Start.Should().Be(11);
            result[0].End.Should().Be(28);

            result[1].Start.Should().Be(39);
            result[1].End.Should().Be(47);

            result[2].Start.Should().Be(79);
            result[2].End.Should().Be(82);


        }
    }
}
