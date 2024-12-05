var lines = File.ReadAllLines("input.txt").Select(line => line.Trim()).ToList();
var seperationLineIndex = lines.IndexOf("");

var ordering = lines.Slice(0, seperationLineIndex)
    .Select(line => line.Split("|"))
    .GroupBy(k => int.Parse(k[0]), v => int.Parse(v[1]))
    .ToDictionary(k => k.Key, v => v.ToList());
var reverseOrdering =  lines.Slice(0, seperationLineIndex)
    .Select(line => line.Split("|"))
    .GroupBy(k => int.Parse(k[1]), v => int.Parse(v[0]))
    .ToDictionary(k => k.Key, v => v.ToList());
var pages = lines.Slice(seperationLineIndex + 1, lines.Count - seperationLineIndex - 1)
    .Select(line => line.Split(","))
    .Select(line => line.Select(int.Parse).ToArray());

var notCorrectlyOrdered = pages
    .Where(pages => !IsCorrectlyOrdered(pages)).ToList();

var fixedOrdered = notCorrectlyOrdered.Select(pages => FixOrder(pages));

var middles = fixedOrdered.Select(pages => pages[pages.Length / 2]).Sum();

Console.WriteLine($"Middles: {middles}");

return;

int[] FixOrder(int[] pages)
{
    if (IsCorrectlyOrdered(pages))
    {
        return pages;
    }
    
    for (int i = 0; i < pages.Length; i++)
    {
        for (int j = i; j < pages.Length; j++)
        {
            if (IsKnownOrder(ordering, (pages[j], pages[i])))
            {
                //We need to swap the values at index i and j
                (pages[i], pages[j]) = (pages[j], pages[i]);
                return FixOrder(pages);
            }
        }
    }
    
    return pages;
}

bool IsCorrectlyOrdered(int[] pages)
{
    return pages
        .SelectMany((v, i) => CreatePageCombinations(pages, i))
        .All(pageCombination => IsValidOrder(ordering, pageCombination) &&
                                !IsKnownOrder(ordering, (pageCombination.Item2, pageCombination.Item1)));
}

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

