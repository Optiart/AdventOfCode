﻿using Tasks.DayEight;
using Tasks.DaySeven;
using Tasks.DaySix;

var days = new Dictionary<int, string>()
{
    {1, "One"},
    {2, "Two"},
    {3, "Three"},
    {4, "Four"},
    {5, "Five"},
    {6, "Six"},
    {7, "Seven"},
    {8, "Eight"}
};

//Console.Write("Run day: ");
//var dayToRun = int.Parse(Console.ReadLine());
var dayToRun = 8;

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