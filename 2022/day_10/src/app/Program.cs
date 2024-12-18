using System.Diagnostics;

var data = File.ReadAllLines(@"/Users/jonathanmoosekian/Projects/AdventOfCode/2022/day_10/input.txt");

var register = new Pixel(1);

var index = 0;
var cycle = 0;
var lines = 0;
var signals = new List<int>();

(string Instruction, int Amount) = ("", 0); 


/*
 * At the start of the first cycle, the noop instruction begins execution. 
 * During the first cycle, X is 1. 
 * After the first cycle, the noop instruction finishes execution, doing nothing.
 * 
 * At the start of the second cycle, the addx 3 instruction begins execution. 
 * During the second cycle, X is still 1.
 * 
 * During the third cycle, X is still 1. 
 * After the third cycle, the addx 3 instruction finishes execution, setting X to 4.
 * 
 * At the start of the fourth cycle, the addx -5 instruction begins execution. 
 * During the fourth cycle, X is still 4.
 * 
 * During the fifth cycle, X is still 4. 
 * After the fifth cycle, the addx -5 instruction finishes execution, setting X to -1.
 *
 * addx V takes two cycles to complete. 
 * After two cycles, the X register is increased by the value V. (V can be negative.)
 * 
 * noop takes one cycle to complete. It has no other effect.
 * 
 */

while (true)
{
    if (cycle * (lines + 1) >= 240) break;

    (Instruction, Amount) = ParseInstruction(data[index++]);

    IncrementCycle();

    // if noop do nothing
    if (Instruction == "noop")
    {
        continue;
    }

    IncrementCycle();

    // if value to add, update register
    register.Update(Amount);

    //Console.WriteLine($"{instruction} with amount: {amount}");
    //Console.WriteLine($"Register: {register}, Cycle: {cycle}, Signal: {signal}");
}

void TryUpdateSignal()
{
    var pixel = register.Positions.Contains(cycle - 1) ? "#" : ".";
    Console.Write($"{pixel}");
}

void IncrementCycle()
{
    if (cycle > 0 && cycle % 40 == 0)
    {
        cycle = 1;
        lines += 1;
        Console.WriteLine();
    } else
    {
        cycle += 1;
    }
    TryUpdateSignal();
}

(string Instruction, int Amount) ParseInstruction(string instruction)
{
    var parts = instruction.Split(' ');

    if (parts[0] == "noop")
        return (parts[0], 0);

    return (parts[0], int.Parse(parts[1]));
}

[DebuggerDisplay("{center - 1} {center} {center + 1}")]
internal class Pixel
{
    private int center;

    public Pixel(int initialCenter)
    {
        center = initialCenter;
    }

    public IEnumerable<int> Positions { get => new[] { center - 1, center, center + 1 }; }

    public void Update(int newCenter) => center += newCenter;
}