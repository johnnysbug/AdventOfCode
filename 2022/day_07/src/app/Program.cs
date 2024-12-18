using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

var data = File.ReadAllLines(@"/Users/jonathanmoosekian/Projects/AdventOfCode/2022/day_07/input.txt");

var commandRegEx = CommandRegex();
var directoryRegEx = FolderRegex();
var fileRegEx = FileRegex();

var root = new Folder { Name = "/" };

var currentFolder = root;

foreach (var line in data)
{
    if (commandRegEx.IsMatch(line))
    {
        var match = commandRegEx.Match(line);
        var cmd = match.Groups["command"].Value;
        var arg = match.Groups["arg"].Value;

        if (cmd == "cd")
        {
            switch (arg)
            {
                case "..":
                    currentFolder = currentFolder?.Parent;
                    break;
                case "/":
                    break;
                default:
                    // folder name
                    currentFolder = currentFolder?.SubFolders.FirstOrDefault(f => f.Name == arg);
                    break;
            }
        }
    }
    else if (directoryRegEx.IsMatch(line))
    {
        var match = directoryRegEx.Match(line);

        var dir = new Folder
        {
            Name = match.Groups["directory"].Value,
            Parent = currentFolder
        };
        currentFolder?.SubFolders.Add(dir);
    }
    else if (fileRegEx.IsMatch(line))
    {
        var match = fileRegEx.Match(line);
        var file = new SizedFile
        {
            Name = match.Groups["fileName"].Value,
            Size = int.Parse(match.Groups["size"].Value)
        };
        currentFolder?.Files.Add(file);
    }
}

var target = 30000000 - (70000000 - root.Size);

var toDelete = root
    .Flatten()
    .Select(f => f.Size)
    .OrderBy(s => s)
    .First(s => s > target);

Console.WriteLine($"Folder to Delete Size: {toDelete}");

Console.ReadKey();

class SizedFile
{
    public string Name { get; set; } = "";

    public int Size { get; set; }
}

[DebuggerDisplay("{Size}", Name = "{Name}")]
class Folder
{
    public string Name { get; set; } = "";

    public int Size => Files.Sum(f => f.Size) + SubFolders.Sum(f => f.Size);

    public bool Include => Size <= 100000;

    public Folder? Parent { get; set; }

    public List<Folder> SubFolders { get; } = new List<Folder>();

    public List<SizedFile> Files { get; } = new List<SizedFile>();

    public IEnumerable<Folder> Flatten() => SubFolders.Concat(SubFolders.SelectMany(f => f.Flatten()));
}

partial class Program
{
    [GeneratedRegex("(?<prompt>\\$).(?<command>\\w.)\\s(?<arg>\\S*)", RegexOptions.Compiled)]
    private static partial Regex CommandRegex();

    [GeneratedRegex("(?<dir>dir)\\s(?<directory>.*)", RegexOptions.Compiled)]
    private static partial Regex FolderRegex();

    [GeneratedRegex("(?<size>\\d+)\\s(?<fileName>.*)", RegexOptions.Compiled)]
    private static partial Regex FileRegex();
}
