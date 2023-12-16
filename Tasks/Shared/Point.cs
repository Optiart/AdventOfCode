namespace Tasks.Shared
{
    public readonly struct Point(int i, int j)
    {
        public int I { get; } = i;

        public int J { get; } = j;

        public override string ToString() => $"[{I},{J}]";
    }
}
