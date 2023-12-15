using Tasks.DayEight;
using Tasks.DayEleven;
using Tasks.DayFifteen;
using Tasks.DayFourteen;
using Tasks.DayNine;
using Tasks.DaySeven;
using Tasks.DaySix;
using Tasks.DayTen;
using Tasks.DayThirteen;
using Tasks.DayTwelve;

var days = new Dictionary<int, string>()
{
    {1, "One"},
    {2, "Two"},
    {3, "Three"},
    {4, "Four"},
    {5, "Five"},
    {6, "Six"},
    {7, "Seven"},
    {8, "Eight"},
    {9, "Nine"},
    {10, "Ten"},
    {11, "Eleven"},
    {12, "Twelve"},
    {13, "Thirteen"},
    {14, "Fourteen"},
    {15, "Fifteen"}
};

//Console.Write("Run day: ");
//var dayToRun = int.Parse(Console.ReadLine());
var dayToRun = 15;

var inputLines = File.ReadAllLines(@$"Day{days[dayToRun]}\Input.txt");

if (dayToRun == 1)
{
    RunAndMeasure(
        () => Trebuchet.GetSumOfCalibrationValues(inputLines, includeStringNumbers: false),
        () => Trebuchet.GetSumOfCalibrationValues(inputLines, includeStringNumbers: true));
}

if (dayToRun == 2)
{
    RunAndMeasure(
        () => new CubeConundrum(inputLines).CalculateSumOfPossibleGameIds(),
        () => new CubeConundrum(inputLines).CalculateSumOfPowerOfFewestNumberOfCubesNeeded());
}

if (dayToRun == 3)
{
    RunAndMeasure(
        () => GearRatios.CalculateSum(inputLines),
        () => GearRatios.CalculateRatioSum(inputLines));
}

if (dayToRun == 4)
{
    RunAndMeasure(
        () => Scratchcards.CalculateWinningNumbers(inputLines),
        () => Scratchcards.CalculateWinningCopyOfCardsWithMemo(inputLines));
}

if (dayToRun == 5)
{
    RunAndMeasure(
        () => new Fertilizer(inputLines).FindMinimumLocation(),
        () => new Fertilizer(inputLines).FindMinimumLocationForSeedRanges());
}

if (dayToRun == 6)
{
    RunAndMeasure(
        () => new WaitForIt(inputLines, hasKerning: false).CalculateWaysToWinWithMath(),
        () => new WaitForIt(inputLines, hasKerning: true).CalculateWaysToWinWithMath());
}

if (dayToRun == 7)
{
    RunAndMeasure(
        () => new CamelCards(inputLines, isJokerUsed: false).CalculateTotalWinnings(),
        () => new CamelCards(inputLines, isJokerUsed: true).CalculateTotalWinnings());
}

if (dayToRun == 8)
{
    RunAndMeasure(
        () => new HauntedWasteland(inputLines).FindStepsToDestination(),
        () => new HauntedWasteland(inputLines).FindStepsToDestinationAsGhostViaLcm());
}

if (dayToRun == 9)
{
    RunAndMeasure(
        () => new MirageMaintenance(inputLines).CalculateSumOfNextPredictedValues(),
        () => new MirageMaintenance(inputLines).CalculateSumOfPreviousPredictedValues());
}

if (dayToRun == 10)
{
    RunAndMeasure(
        () => new PipeMaze(inputLines).CalculateFarthestNumberOfSteps(),
        () => new PipeMaze(inputLines, scaleMap: true).CalculateEnclosedTiles());
}

if (dayToRun == 11)
{
    RunAndMeasure(
        () => new CosmicExpansion(inputLines).FindShortestPathSum(),
        () => new CosmicExpansion(inputLines).FindShortestPathSum(expansionDistance: 1000000));
}

if (dayToRun == 12)
{
    RunAndMeasure(
        () => new HotSprings(inputLines, isFolded: true).CalculateSumOfArrangements(),
        () => 1);
}

if (dayToRun == 13)
{
    RunAndMeasure(
        () => new PointOfIncidence(inputLines).CalculateReflections(),
        () => new PointOfIncidence(inputLines).CalculateReflections(hasSmudge: true));
}

if (dayToRun == 14)
{
    RunAndMeasure(
        () => new ParabolicReflectorDish(inputLines).CalculateNorthLoad(),
        () => new ParabolicReflectorDish(inputLines).CalculateNorthLoadWithCycled());
}

if (dayToRun == 15)
{
    RunAndMeasure(
        () => new LensLibrary(inputLines).ComputeInitializationSequenceSum(),
        () => new LensLibrary(inputLines).ComputeFocusingPower());
}
static void RunAndMeasure<T>(Func<T> funcPart1, Func<T> funcPart2)
{
    const string partOne = "Part One";
    const string partTwo = "Part Two";

    var stopWatch = new Stopwatch();
    stopWatch.Start();
    var part1Result = funcPart1();
    stopWatch.Stop();
    Console.WriteLine($"{partOne}:\t{stopWatch.Elapsed.TotalMilliseconds}ms\t{part1Result}");

    var totalTime = stopWatch.Elapsed;

    stopWatch.Restart();
    var part2Result = funcPart2();
    stopWatch.Stop();
    Console.WriteLine($"{partTwo}:\t{stopWatch.Elapsed.TotalMilliseconds}ms\t{part2Result}");
    Console.WriteLine($"Total:\t {totalTime.Add(stopWatch.Elapsed).TotalMilliseconds}ms");
}