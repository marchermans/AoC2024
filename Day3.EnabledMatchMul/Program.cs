// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

const string MUL_REGEX_PATTERN = "mul\\((?<left>[0-9]{1,3}),(?<right>[0-9]{1,3})\\)";
var mulMatcher = new Regex(MUL_REGEX_PATTERN);

const string DISABLED_REPLACE_PATTERN = "don\\'t\\(\\).*?do\\(\\)";
var replacementMatcher = new Regex(DISABLED_REPLACE_PATTERN);

var result = File
    .ReadAllLines("input.txt")
    .Aggregate("", (current, line) => current + line, result => new[] {result})
    .Select(line => replacementMatcher.Replace(line, ""))
    .SelectMany(line => mulMatcher.Matches(line))
    .Where(m => m.Success)
    .GroupBy(m => m.Groups["left"].Value)
    .SelectMany(m => m.Select(v => int.Parse(v.Groups["right"].Value) * int.Parse(m.Key)))
    .Sum();

Console.WriteLine("Result: " + result);