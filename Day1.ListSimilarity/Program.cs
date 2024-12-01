// See https://aka.ms/new-console-template for more information

var left = new List<int>();
var right = new List<int>();

var input = File.ReadAllLines("input.csv");
foreach (var line in input)
{
    var sections = line.Split(",");
    if (!int.TryParse(sections[0], out var x) || !int.TryParse(sections[1], out var y))
    {
        Console.WriteLine($"Invalid input: {line}");
        throw new Exception();
    }
    
    left.Add(x);
    right.Add(y);
}

var rightCount = new Dictionary<int, int>();

foreach (var y in right)
{
    if (rightCount.ContainsKey(y))
    {
        rightCount[y]++;
    }
    else
    {
        rightCount[y] = 1;
    }
}

var similarity = 0;

foreach (var x in left)
{
    if (rightCount.ContainsKey(x) && rightCount[x] > 0)
    {
        similarity += (x * rightCount[x]);
    }
}

Console.WriteLine(similarity);