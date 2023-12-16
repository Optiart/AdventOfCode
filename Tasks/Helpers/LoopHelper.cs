namespace Tasks.Helpers
{
    public static class LoopHelper
    {
        public static void Run2DLoop(this string[] stringArray, Action<int, int, string[]> action)
        {
            for (var i = 0; i < stringArray.Length; i++)
            {
                for (var j = 0; j < stringArray[i].Length; j++)
                {
                    action(i, j, stringArray);
                }
            }
        }

        public static void Run2DLoop<T>(this T[,] map, Action<int, int, T[,]> action)
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    action(i, j, map);
                }
            }
        }
    }
}
