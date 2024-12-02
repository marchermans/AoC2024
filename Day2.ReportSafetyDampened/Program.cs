// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;

IEnumerable<int[]> EliminateLevelIn(int[] input)
{
    yield return input;
    
    var length = input.Length;

    if (length < 1)
    {
        yield break;
    }
    
    for (var i = 0; i < length; i++)
    {
        var newArray = new int[length - 1];
        var x = 0;
        for (var j = 0; j <= length - 1; j++)
        {
            if (j == i) j++;
            if (j == length) break;
            newArray[x] = input[j];
            x++;
        }
        yield return newArray;
    }
}

Console.WriteLine("Safe report count: " + File
    .ReadAllLines("input.csv")
    .Select(l => l.Split(' ').Select(int.Parse).ToArray())
    .Select(EliminateLevelIn)
    .Count(variants => variants
        .Select(metrics => metrics[..^1].Select((value, index) => value - metrics[index +1 ]).ToList())
        .Where(diffs => diffs.All(diff => Math.Abs(diff) <= 3 &&  1 <= Math.Abs(diff)))
        .Any(diffs => diffs.All(diff => diff > 0) || diffs.All(diff => diff < 0)))); 