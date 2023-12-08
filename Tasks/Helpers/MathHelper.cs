namespace Tasks.Helpers
{
    public static class MathHelper
    {
        public static long CalculateLeastCommonMultiple(int[] input)
        {
            if (input.Length == 0)
            {
                throw new ArgumentException("So numbers were supplied");
            }

            if (input.Length < 2)
            {
                return input[0];
            }

            var currentLcm = CalculateLeastCommonMultiple(input[0], input[1]);
            for (var i = 2; i < input.Length; i++)
            {
                currentLcm = CalculateLeastCommonMultiple(input[i], currentLcm);
            }

            return currentLcm;
        }

        public static long CalculateLeastCommonMultiple(long numA, long numB) 
            => (numA * numB) / CalculateGreatestCommonDivisor(numA, numB);

        public static long CalculateGreatestCommonDivisor(long numA, long numB) =>
            numB == 0 ? numA : CalculateGreatestCommonDivisor(numB, numA % numB);
    }
}
