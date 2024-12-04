// See https://aka.ms/new-console-template for more information

var lines = File.ReadAllLines("input.txt");
var lineWidth = lines[0].Length;
if (lines.Any(l => l.Length != lineWidth))
{
    throw new Exception("Lines are not all the same length");
}

var matrix = new char[lines.Length][];
for (var i = 0; i < lines.Length; i++)
{
    matrix[i] = lines[i].ToCharArray();
}

var xmasCount = 0;
for (var i = 0; i < matrix.Length; i++)
{
    for (var j = 0; j < matrix[i].Length; j++)
    {
        xmasCount += IsCrossMas(matrix, i, j) ? 1 : 0;
    }
}

Console.WriteLine(xmasCount);

return;


bool Is(char[][] characters, int x, int y, char c)
{
    if (x < 0 || x >= characters.Length)
    {
        return false;
    }

    if (y < 0 || y >= characters[0].Length)
    {
        return false;
    }

    return characters[x][y] == c;
}

bool IsCrossMas(char[][] characters, int x, int y)
{
    if (!Is(characters, x, y, 'A'))
        return false;
    
    //We are looking at the diagonals, on each corner of the 'A' there needs to be either an
    //'M' or an 'S' respectively for it to be a crossmas:
    // M..
    // .A.
    // ..S

    return IsFirstDiagonalMas(characters, x - 1, y - 1) && IsSecondDiagonalMas(characters, x - 1, y + 1);
}

bool IsFirstDiagonalMas(char[][] characters, int x, int y)
{
    if (Is(characters, x, y, 'M') &&
        Is(characters, x + 1, y + 1, 'A') &&
        Is(characters, x + 2, y + 2, 'S'))
        return true;
    
    return Is(characters, x, y, 'S') &&
           Is(characters, x + 1, y + 1, 'A') &&
           Is(characters, x + 2, y + 2, 'M');
}

bool IsSecondDiagonalMas(char[][] characters, int x, int y)
{
    if (Is(characters, x, y, 'M') &&
        Is(characters, x + 1, y - 1, 'A') &&
        Is(characters, x + 2, y - 2, 'S'))
        return true;
    
    return Is(characters, x, y, 'S') &&
           Is(characters, x + 1, y - 1, 'A') &&
           Is(characters, x + 2, y - 2, 'M');
}