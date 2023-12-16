namespace Tasks.Helpers
{
    public static class InputReader
    {
        public static char[,] ReadToCharMap(string[] inputLines)
        {
            var map = new char[inputLines.Length, inputLines[0].Length];
            inputLines.Run2DLoop((i, j, input) => map[i, j] = input[i][j]);
            return map;
        }
    }
}
