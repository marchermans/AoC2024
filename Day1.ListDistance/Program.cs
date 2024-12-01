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

left.Sort();
right.Sort();

var distance = 0;
for (var i = 0; i < left.Count; i++)
{
    distance += Math.Abs(left[i] - right[i]);
}

Console.WriteLine(distance);