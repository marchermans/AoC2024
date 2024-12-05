var lines = File.ReadAllLines("input.txt").Select(line => line.Trim()).ToList();
var seperationLineIndex = lines.IndexOf("");

var ordering = lines.Slice(0, seperationLineIndex)
    .Select(line => line.Split("|"))
    .GroupBy(k => int.Parse(k[0]), v => int.Parse(v[1]))
    .ToDictionary(k => k.Key, v => v.ToList());
var pages = lines.Slice(seperationLineIndex + 1, lines.Count - seperationLineIndex - 1)
    .Select(line => line.Split(","))
    .Select(line => line.Select(int.Parse).ToArray());

var correctlyOrdered = pages
    .Where(pages => pages
        .SelectMany((v, i) => CreatePageCombinations(pages, i))
        .All(pageCombination => IsValidOrder(ordering, pageCombination) &&
                                !IsKnownOrder(ordering, (pageCombination.Item2, pageCombination.Item1)))
    ).ToList();

var middles = correctlyOrdered.Select(pages => pages[pages.Length / 2]).Sum();


Console.WriteLine($"Middles: {middles}");

return;
    
IEnumerable<(int, int)> CreatePageCombinations(int[] pages, int index)
{
    if (index == pages.Length - 1) yield break;

    for (var i = index + 1; i < pages.Length; i++)
    {
        yield return (pages[index], pages[i]);
    }
}

bool IsValidOrder(Dictionary<int, List<int>> ordering, (int, int) pageCombo)
{
    return !ordering.ContainsKey(pageCombo.Item1) || IsKnownOrder(ordering, pageCombo);
}

bool IsKnownOrder(Dictionary<int, List<int>> ordering, (int, int) pageCombo)
{
    return ordering.ContainsKey(pageCombo.Item1) && ordering[pageCombo.Item1].Contains(pageCombo.Item2);
}
