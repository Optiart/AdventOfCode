namespace Tasks.DayNine
{
    public class MirageMaintenance
    {
        private readonly int[][] _sequences;

        public MirageMaintenance(string[] inputLines)
        {
            _sequences = new int[inputLines.Length][];

            for (var i = 0; i < inputLines.Length; i++)
            {
                _sequences[i] = inputLines[i]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }
        }

        public int CalculateSumOfNextPredictedValues() => _sequences.Sum(s => PredictNextValue(s, 0));

        public int CalculateSumOfPreviousPredictedValues() => _sequences.Sum(s => PredictValueBeforeTheFirst(s, 0));

        private int PredictNextValue(int[] sequence, int depth)
        {
            var allZeros = true;
            for (var i = 1; i < sequence.Length - depth; i++)
            {
                sequence[i - 1] = sequence[i] - sequence[i - 1];
                if (sequence[i - 1] != 0)
                {
                    allZeros = false;
                }
            }

            if (allZeros)
            {
                return sequence.Sum();
            }

            return PredictNextValue(sequence, depth + 1);
        }

        private int PredictValueBeforeTheFirst(int[] sequence, int depth)
        {
            var allZeros = true;
            for (var i = sequence.Length - 1; i > depth; i--)
            {
                sequence[i] = sequence[i - 1] - sequence[i];
                if (sequence[i] != 0)
                {
                    allZeros = false;
                }
            }

            if (allZeros)
            {
                return sequence.Sum();
            }

            return PredictValueBeforeTheFirst(sequence, depth + 1);
        }
    }
}
