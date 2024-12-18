using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

var data = File.ReadAllText(@"/Users/jonathanmoosekian/Projects/AdventOfCode/2022/day_11/test.txt");

var pattern = @".+\d+\:\n.+: (?<Items>.+)\n.+: new = (?<Operation>.+)\n.+ (?<Test>\d+)\n.+(?<True>\d+)\n.+(?<False>\d+)";
var regex = new Regex(pattern, RegexOptions.Compiled);

var matches = regex.Matches(data);

var monkeys = matches
    .Select(m => new Monkey(
        m.Groups["Operation"].Value,
        int.Parse(m.Groups["Test"].Value),
        int.Parse(m.Groups["True"].Value),
        int.Parse(m.Groups["False"].Value),
        m.Groups["Items"].Value.Split(", ").Select(i => long.Parse(i)).ToList()))
    .ToList();

for (int i = 0; i < 10000; i++)
{
    int index = 0;
    foreach (var monkey in monkeys)
    {
        Console.WriteLine($"Monkey {index}:");

        while (monkey.Worries.Count > 0)
        {
            (int throwTo, long item) = monkey.Inspect();
            monkeys[throwTo].Catch(item);
        }
        index++;
    }

    Console.WriteLine();
    Console.WriteLine();

    for (int m = 0; m < monkeys.Count; m++)
    {
        Console.Write($"Monkey {m}: ");
        for (int item = 0; item < monkeys[m].Worries.Count; item++)
        {
            if (item > 0)
                Console.Write(", ");
            Console.Write($"{monkeys[m].Worries[item]}");
        }
        Console.WriteLine();
    }

    Console.WriteLine();

    switch (i)
    {
        case 0:
        case 19:
        case 999:
        case 1999:
        case 3000:
        case 4000:
        case 5000:
        case 6000:
        case 7000:
        case 8000:
        case 9000:
        case 10000:
            Console.WriteLine($"== After round {i} ==");
            for (int m = 0; m < monkeys.Count; m++)
            {
                Console.WriteLine($"Monkey {m} inspected items {monkeys[m].Inspections} times.");
            }
            Console.WriteLine();
            break;
        default:
            break;
    }
}

var topTwo = monkeys.Select(m => m.Inspections).OrderByDescending(i => i).Take(2).ToList();

Console.WriteLine($"Monkey Business Level: {topTwo[0] * topTwo[1]}");

internal class Monkey
{
    private string[] _operationParts;

    public Monkey(string operation, long test, int trueValue, int falseValue, List<long> worries)
    {
        _operationParts = operation.Split(" ");

        Test = test;
        TrueValue = trueValue;
        FalseValue = falseValue;
        Worries = worries;
    }

    public List<long> Worries { get; }
    public long Test { get; }
    public int TrueValue { get; }
    public int FalseValue { get; }

    public long Inspections { get; private set; } = 0;

    public (int ThrowToMonkey, long Item) Inspect()
    {
        Inspections++;
        var old = Worries[0];
        Console.WriteLine($"  Monkey inspects an item with a worry level of {old}.");

        var left = _operationParts[0] == "old" ? old : long.Parse(_operationParts[0]);
        var right = _operationParts[2] == "old" ? old : long.Parse(_operationParts[2]);

        long result = 0;

        if (_operationParts[1] == "+")
        {
            result = left + right;
            Console.WriteLine($"    Worry level increases by {right} to {result}.");
        }
        else
        {
            result = left * right;
            if (left == right)
                Console.WriteLine($"    Worry level is multiplied by itself to {result}.");
            else
                Console.WriteLine($"    Worry level is multiplied by {right} to {result}.");
        };

        int throwTo;
        if (result % Test == 0)
        {
            Console.WriteLine($"    Current worry level is divisible by {Test}.");
            throwTo = TrueValue;
        }
        else
        {
            Console.WriteLine($"    Current worry level is not divisible by {Test}.");
            throwTo = FalseValue;
        }

        Worries.RemoveAt(0);

        Console.WriteLine($"    Item with worry level {result} is thrown to monkey {throwTo}.");
        return (throwTo, result);
    }   

    public void Catch(long amount)
    {
        Worries.Add(amount);
    }
}