using FluentAssertions;
using Tasks.DaySeven;

namespace Tests
{
    public class DaySevenTests
    {
        [Theory]
        [InlineData("23456 1", CamelCards.HighCard)]
        [InlineData("234AA 1", CamelCards.OnePair)]
        [InlineData("K2KAA 1", CamelCards.TwoPairs)]
        [InlineData("K2KAK 1", CamelCards.ThreeOfAKind)]
        [InlineData("KJJJ2 1", CamelCards.ThreeOfAKind)]
        [InlineData("KAKAK 1", CamelCards.FullHouse)]
        [InlineData("KAKKK 1", CamelCards.FourOfAKind)]
        [InlineData("KKKKK 1", CamelCards.FiveOfAKind)]
        public void DeterminesCorrectHandComboWeight(string inputLine, int expectedWeight)
        {
            // Arrange
            var inputLines = new[] { inputLine };

            // Act
            var camelCards = new CamelCards(inputLines);

            // Assert
            var hands = camelCards.Hands;
            hands.Count.Should().Be(1);

            var hand = hands.First();
            hand.Combo.Should().Be(expectedWeight);
        }

        [Theory]
        [InlineData("2T9JQ 1", CamelCards.OnePair)]
        [InlineData("229QQ 1", CamelCards.TwoPairs)]
        [InlineData("J844T 1", CamelCards.ThreeOfAKind)]
        [InlineData("2TJJQ 1", CamelCards.ThreeOfAKind)]
        [InlineData("2TQQQ 1", CamelCards.ThreeOfAKind)]
        [InlineData("22J44 1", CamelCards.FullHouse)]
        [InlineData("2TJJJ 1", CamelCards.FourOfAKind)]
        [InlineData("JTJJJ 1", CamelCards.FiveOfAKind)]
        [InlineData("JJJJJ 1", CamelCards.FiveOfAKind)]
        public void DeterminesCorrectHandComboWeightWithJoker(string inputLine, int expectedWeight)
        {
            // Arrange
            var inputLines = new[] { inputLine };

            // Act
            var camelCards = new CamelCards(inputLines, isJokerUsed: true);

            // Assert
            var hands = camelCards.Hands;
            hands.Count.Should().Be(1);

            var hand = hands.First();
            hand.Combo.Should().Be(expectedWeight);
        }

        [Fact]
        public void CalculatesTotalWinnings()
        {
            // Arrange
            var inputLines = new[]
            {
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483"
            };
            const int expectedResult = 6440;

            var camelCards = new CamelCards(inputLines);

            // Act
            var result = camelCards.CalculateTotalWinnings();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CalculatesTotalWinningsForSameCategory()
        {
            // Arrange
            var inputLines = new[]
            {
                "33332 20",
                "2AAAA 30"
            };
            var expectedResult = 20 * 2 + 30 * 1;

            var camelCards = new CamelCards(inputLines);

            // Act
            var result = camelCards.CalculateTotalWinnings();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
