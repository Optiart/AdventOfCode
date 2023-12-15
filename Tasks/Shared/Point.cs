namespace Tasks.Shared
{
    internal readonly struct Point(int i, int j)
    {
        public int I { get; } = i;

        public int J { get; } = j;
    }
}
