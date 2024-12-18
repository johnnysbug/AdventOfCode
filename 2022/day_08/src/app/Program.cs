var data = File.ReadAllLines("input.txt");
var dict = new Dictionary<(int, int), int>();

for (int y = 0; y < data.Length; y++)
{
    var row = data[y].Select(c => int.Parse(c.ToString())).ToArray();

    for (int x = 0; x < data[y].Length; x++)
    {
        var tree = row[x];

        var up =    ScoreToEdge(y - 1, i => i >= 0,          tree,i => int.Parse(data[i].ElementAt(x).ToString()), -1);
        var down =  ScoreToEdge(y + 1, i => i < data.Length, tree,i => int.Parse(data[i].ElementAt(x).ToString()), 1);
        var left =  ScoreToEdge(x - 1, i => i >= 0,          tree, i => row[i], -1);
        var right = ScoreToEdge(x + 1, i => i < row.Length,  tree, i => row[i], 1);

        dict[(x, y)] = up * down * left * right;
    }
}

Console.WriteLine($"Highest Score: {dict.Values.Max()}");

int ScoreToEdge(int start, Func<int, bool> condition, int tree, Func<int, int> valueToCheck, int increment)
{
    int score = 0;
    for (int i = start; condition(i); i += increment)
    {
        if (valueToCheck(i) >= tree)
        {
            score += 1;
            break;
        }
        score += 1;
    }
    return score;
}