
using System.Text.RegularExpressions;

const string MUL_REGEX_PATTERN = "^mul\\((?<left>[0-9]{1,3}),(?<right>[0-9]{1,3})\\)";
var mulMatcher = new Regex(MUL_REGEX_PATTERN);

const string DO = "do()";
const string DONT = "don't()";
const string MULMAX = "mul(123,123)";

var enabled = true;
var current = 0;

//181339741
//182780583
foreach (var line in File.ReadAllLines("input.txt"))
{
    for (var i = 0; i < line.Length; i++)
    {
        var doBlock = line.Substring(i, Math.Min(line.Length - i, DO.Length));
        var dontBlock = line.Substring(i, Math.Min(line.Length - i, DONT.Length));
        var maxMulBlock = line.Substring(i, Math.Min(line.Length - i, MULMAX.Length));

        switch (enabled)
        {
            case true when mulMatcher.IsMatch(maxMulBlock):
            {
                var match = mulMatcher.Match(maxMulBlock);
                var left = int.Parse(match.Groups["left"].Value);
                var right = int.Parse(match.Groups["right"].Value);
            
                current += left * right;
                break;
            }
            case false when doBlock.Equals(DO):
                enabled = true;
                break;
            case true when dontBlock.Equals(DONT):
                enabled = false;
                break;
        } 
    }
}

Console.WriteLine("Result: " + current);