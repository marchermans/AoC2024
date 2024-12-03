// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

const string MUL_REGEX_PATTERN = "mul\\((?<left>[0-9]{1,3}),(?<right>[0-9]{1,3})\\)";
var mulMatcher = new Regex(MUL_REGEX_PATTERN);

var result = File
    .ReadAllLines("input.txt")
    .SelectMany(line => mulMatcher.Matches(line))
    .Where(m => m.Success)
    .GroupBy(m => m.Groups["left"].Value)
    .SelectMany(m => m.Select(v => int.Parse(v.Groups["right"].Value) * int.Parse(m.Key)))
    .Sum();

Console.WriteLine("Result: " + result);
    