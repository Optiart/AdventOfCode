namespace Tasks.DayFifteen
{
    public class LensLibrary
    {
        private readonly string _line;

        public LensLibrary(string[] inputLines) => _line = inputLines.Single();

        public int ComputeInitializationSequenceSum()
        {
            var sum = 0;
            var currentValue = 0;

            foreach (var c in _line)
            {
                if (c == ',')
                {
                    sum += currentValue;
                    currentValue = 0;
                    continue;
                }

                currentValue = ComputeHashValue(c, currentValue);
            }

            sum += currentValue;
            return sum;
        }


        public int ComputeFocusingPower()
        {
            var sum = 0;
            var currentLabelHash = 0;
            var label = string.Empty;
            var boxes = new Dictionary<int, Lens?>();

            for (var i = 0; i < _line.Length; i++)
            {
                var c = _line[i];

                if (c == ',')
                {
                    currentLabelHash = 0;
                    label = string.Empty;
                    continue;
                }

                if (c == '-')
                {
                    boxes.TryGetValue(currentLabelHash, out var storedLens);
                    boxes[currentLabelHash] = RemoveFrontLensFromBox(storedLens, label);
                }
                else if (c == '=')
                {
                    i++;
                    var focalLength = _line[i] - '0';
                    var newLens = new Lens(label, focalLength);
                    boxes.TryGetValue(currentLabelHash, out var storedLens);
                    boxes[currentLabelHash] = AddLensToBox(storedLens, newLens);
                }
                else
                {
                    label += c;
                    currentLabelHash = ComputeHashValue(c, currentLabelHash);
                }
            }

            foreach (var box in boxes)
            {
                var lens = box.Value;
                var slot = 1;
                var boxResult = 0;
                while (lens != null)
                {
                    boxResult += (1 + box.Key) * slot * lens.Value;
                    lens = lens.Next;
                    slot++;
                }

                sum += boxResult;
            }

            return sum;
        }

        private Lens AddLensToBox(Lens? stored, Lens newLens)
        {
            if (stored == null)
            {
                return newLens;
            }

            var head = stored;

            while (stored != null)
            {
                if (stored.Label == newLens.Label)
                {
                    stored.Value = newLens.Value;
                    return head;
                }

                if (stored.Next == null)
                {
                    stored.Next = newLens;
                    return head;
                }

                stored = stored.Next;
            }

            return head;
        }

        private Lens? RemoveFrontLensFromBox(Lens? stored, string targetLabel)
        {
            if (stored == null)
            {
                return null;
            }

            var head = stored;
            Lens? previous = null;
            var current = stored;

            do
            {
                if (current.Label == targetLabel && previous == null)
                {
                    if (current.Next == null)
                    {
                        return null;
                    }

                    current = new Lens(current.Next.Label, current.Next.Value)
                    {
                        Next = current.Next.Next
                    };

                    head = current;
                    continue;
                }

                if (current.Label == targetLabel && previous != null)
                {
                    previous.Next = current.Next;
                    current = current.Next?.Next;
                    continue;
                }

                previous = current;
                current = current.Next;

            } while (current != null);

            return head;
        }

        private int ComputeHashValue(char c, int currentValue)
        {
            var ascii = (int)c;
            currentValue += ascii;
            currentValue *= 17;
            currentValue %= 256;

            return currentValue;
        }

        private class Lens(string label, int value)
        {
            public int Value { get; set; } = value;

            public string Label { get; set; } = label;

            public Lens? Next { get; set; }

            public override string ToString() => $"[{Label} {Value}]";
        }
    }
}
