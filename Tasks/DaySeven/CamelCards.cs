namespace Tasks.DaySeven
{
    public class CamelCards
    {
        public readonly List<Hand> Hands;

        public const int FiveOfAKind = 7;
        public const int FourOfAKind = 6;
        public const int FullHouse = 5;
        public const int ThreeOfAKind = 4;
        public const int TwoPairs = 3;
        public const int OnePair = 2;
        public const int HighCard = 1;

        private const int TenWeight = 10;
        private const int JackWeight = 11;
        private const int QueenWeight = 12;
        private const int KingWeight = 13;
        private const int AceWeight = 14;
        private const int JokerWeight = 1;

        private const int HandCount = 5;

        public CamelCards(string[] inputLines, bool isJokerUsed = false)
        {
            Hands = new List<Hand>(inputLines.Length);
            foreach (var inputLine in inputLines)
            {
                var parts = inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var bid = int.Parse(parts[1]);
                var cardWeights = parts[0].Select(p => p switch
                {
                    'T' => TenWeight,
                    'J' => isJokerUsed ? JokerWeight : JackWeight,
                    'Q' => QueenWeight,
                    'K' => KingWeight,
                    'A' => AceWeight,
                    _ => p - '0'
                }).ToArray();

                Hands.Add(new Hand(cardWeights, bid, isJokerUsed));
            }
        }

        public long CalculateTotalWinnings()
        {
            var rank = Hands.Count;
            long sum = 0;

            var sortedHands = Hands.OrderByDescending(h => h, new HandComparer());

            foreach (var sortedHand in sortedHands)
            {
                sum += sortedHand.Bid * rank;
                rank--;
            }

            return sum;
        }

        public class Hand
        {
            public int[] CardWeights { get; }

            public int Bid { get; }

            public int Combo { get; }

            public bool IsJokerUsed { get; }

            public Hand(int[] cardWeights, int bid, bool isJokerUsed)
            {
                CardWeights = cardWeights;
                IsJokerUsed = isJokerUsed;
                Bid = bid;
                Combo = CalculateComboWeight(cardWeights, isJokerUsed);
            }

            private int CalculateComboWeight(int[] cardWeights, bool isJokerUsed)
            {
                var occurrences = new int[AceWeight + 1];
                var jokerCount = 0;

                foreach (var cardWeight in cardWeights)
                {
                    if (isJokerUsed && cardWeight == JokerWeight)
                    {
                        jokerCount++;
                        continue;
                    }

                    occurrences[cardWeight]++;
                }

                occurrences = occurrences.OrderByDescending(i => i).ToArray();

                var pairs = 0;
                var threeOfAKinds = 0;

                foreach (var occurrence in occurrences)
                {
                    if (jokerCount + occurrence == 5)
                    {
                        return FiveOfAKind;
                    }

                    if (jokerCount + occurrence == 4)
                    {
                        return Math.Min(FourOfAKind, FourOfAKind + jokerCount);
                    }

                    if (jokerCount + occurrence == 3)
                    {
                        threeOfAKinds++;
                    }

                    if (jokerCount + occurrence == 2)
                    {
                        pairs++;
                    }
                }

                if (threeOfAKinds == 2 || (threeOfAKinds == 1 && pairs == 1))
                {
                    return FullHouse;
                }

                if (pairs == 1 || pairs == (HandCount - jokerCount))
                {
                    return OnePair;
                }

                if (threeOfAKinds >= 1)
                {
                    return ThreeOfAKind;
                }

                if (pairs == 2)
                {
                    return TwoPairs;
                }

                return HighCard;
            }
        }

        public class HandComparer : IComparer<Hand>
        {
            public int Compare(Hand x, Hand y)
            {
                if (x.Combo > y.Combo)
                {
                    return 1;
                }

                if (x.Combo < y.Combo)
                {
                    return -1;
                }

                var cardIndex = 0;
                while (cardIndex < HandCount)
                {
                    if (x.CardWeights[cardIndex] > y.CardWeights[cardIndex])
                    {
                        return 1;
                    }

                    if (x.CardWeights[cardIndex] < y.CardWeights[cardIndex])
                    {
                        return -1;
                    }

                    cardIndex++;
                }

                return 0;
            }
        }
    }
}
