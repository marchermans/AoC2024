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
        xmasCount += GetXMasCount(matrix, i, j);
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

int GetXMasCount(char[][] characters, int x, int y)
{
    if (!Is(characters, x, y, 'X'))
    {
        return 0;
    }

    var count = 0;
    
    //Check DOWN
    if(Is(characters, x + 1, y, 'M') && Is(characters, x + 2, y, 'A') && Is(characters, x + 3, y, 'S'))
    {
        count++;
    }
    
    //Check UP
    if(Is(characters, x - 1, y, 'M') && Is(characters, x - 2, y, 'A') && Is(characters, x - 3, y, 'S'))
    {
        count++;
    }
    
    //Check RIGHT
    if(Is(characters, x, y + 1, 'M') && Is(characters, x, y + 2, 'A') && Is(characters, x, y + 3, 'S'))
    {
        count++;
    }
    
    //Check LEFT
    if(Is(characters, x, y - 1, 'M') && Is(characters, x, y - 2, 'A') && Is(characters, x, y - 3, 'S'))
    {
        count++;
    }
    
    //Check DOWN RIGHT
    if(Is(characters, x + 1, y + 1, 'M') && Is(characters, x + 2, y + 2, 'A') && Is(characters, x + 3, y + 3, 'S'))
    {
        count++;
    }
    
    //Check DOWN LEFT
    if(Is(characters, x + 1, y - 1, 'M') && Is(characters, x + 2, y - 2, 'A') && Is(characters, x + 3, y - 3, 'S'))
    {
        count++;
    }
    
    //Check UP RIGHT
    if(Is(characters, x - 1, y + 1, 'M') && Is(characters, x - 2, y + 2, 'A') && Is(characters, x - 3, y + 3, 'S'))
    {
        count++;
    }
    
    //Check UP LEFT
    if(Is(characters, x - 1, y - 1, 'M') && Is(characters, x - 2, y - 2, 'A') && Is(characters, x - 3, y - 3, 'S'))
    {
        count++;
    }
    
    return count;
}