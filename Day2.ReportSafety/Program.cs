// See https://aka.ms/new-console-template for more information

Console.WriteLine("Safe report count: " + File
    .ReadAllLines("input.csv")
    .Select(l => l.Split(' ').Select(int.Parse).ToArray())
    .Select(metrics => metrics[..^1].Select((value, index) => value - metrics[index +1 ]).ToList())
    .Where(diffs => diffs.All(diff => Math.Abs(diff) <= 3 &&  1 <= Math.Abs(diff)))
    .Count(diffs => diffs.All(diff => diff > 0) || diffs.All(diff => diff < 0))); 