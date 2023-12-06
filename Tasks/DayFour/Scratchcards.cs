using System.Collections.Generic;

namespace Tasks.DayFour
{
    public class Scratchcards
    {
        public static int CalculateWinningNumbers(string[] inputLines)
        {
            var cards = ParseCards(inputLines).ToArray();
            var sum = 0;

            foreach (var card in cards)
            {
                var matches = card.WinningNumbers.Intersect(card.Numbers).Count();
                if (matches == 0)
                {
                    continue;
                }

                var result = (int)Math.Pow(2, matches - 1);
                sum += result;
            }

            return sum;
        }

        public static int CalculateWinningCopyOfCardsWithMemo(string[] inputLines)
        {
            var cards = ParseCards(inputLines).ToDictionary(c => c.Id, c => c);
            var memo = new Dictionary<int, int>();
            var copiesSum = 0;

            for (int i = cards.Count; i > 0; i--)
            {
                copiesSum += ProcessCardWithMemo(cards, memo, 0, cards[i]);
            }

            return copiesSum + cards.Keys.Count;
        }

        private static int ProcessCardWithMemo(Dictionary<int, Card> cards, Dictionary<int, int> memo, int copiesCount, Card card)
        {
            if (memo.ContainsKey(card.Id))
            {
                return memo[card.Id];
            }

            var matches = card.WinningNumbers.Intersect(card.Numbers).Count();
            if (matches == 0)
            {
                return 0;
            }

            copiesCount += matches;
            var dependentCopies = 0;

            for (int i = 1; i <= matches; i++)
            {
                var hasNextCard = cards.TryGetValue(card.Id + i, out var copyCard);

                if (!hasNextCard)
                {
                    continue;
                }

                dependentCopies += ProcessCardWithMemo(cards, memo, copiesCount, copyCard);
            }

            memo.Add(card.Id, copiesCount + dependentCopies);
            return copiesCount + dependentCopies;
        }

        private static IEnumerable<Card> ParseCards(string[] inputLines)
        {
            foreach (var line in inputLines)
            {
                var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
                var cardParts = parts[1].Split("|", StringSplitOptions.RemoveEmptyEntries);

                yield return new Card()
                {
                    Id = id,
                    WinningNumbers = cardParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),
                    Numbers = cardParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                };
            }
        }
    }

    public class Card
    {
        public int Id { get; set; }

        public int[] WinningNumbers { get; set; }

        public int[] Numbers { get; set; }
    }
}
