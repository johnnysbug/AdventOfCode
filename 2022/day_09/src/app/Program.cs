    using System.Diagnostics;

    var data = File.ReadAllLines(@"input.txt");

    var head = new Knot();
    var knots = new List<Knot>
    {
        new Knot(), new Knot(), new Knot(), new Knot(),
        new Knot(), new Knot(), new Knot(), new Knot(), new Knot()
    };

    var dict = new Dictionary<(int, int), int> { { (0, 0), 1 } };

    for (int j = 0; j < data.Length; j++)
    {
        string? instruction = data[j];
        var ((x, y), steps) = ParseInstruction(instruction);

        for (int i = 0; i < steps; i++)
        {
            head.X = head.X + x;
            head.Y = head.Y + y;

            Knot currentKnot = head;

            for (int k = 0; k < 9; k++)
            {
                UpdatePosition(currentKnot, knots[k], x, y);
                currentKnot = knots[k];
            }
        
            dict[(currentKnot.X, currentKnot.Y)] = 1;
        }
    }

    Console.WriteLine($"Positions touched: {dict.Keys.Count}");

    ((int X, int Y) Direction, int Steps) ParseInstruction(string instruction)
    {
        var parts = instruction.Split(' ');
        var direction = parts[0] switch
        {
            "L" => Direction.Left,  // -1,  0
            "R" => Direction.Right, //  1,  0
            "U" => Direction.Up,    //  0, -1
            "D" => Direction.Down,  //  0,  1
            _ => Direction.None
        };

        return (direction, int.Parse(parts[1]));
    }

    bool IsTouching(Knot knot, Knot nextKnot)
    {
        var xDiff = Math.Abs(knot.X - nextKnot.X);
        var yDiff = Math.Abs(knot.Y - nextKnot.Y);
        var diff = Math.Abs(xDiff - yDiff);

        return (xDiff == 0 || xDiff == 1) && (yDiff == 0 || yDiff == 1);
    }

    (int X, int Y) GetDirection(Knot fromKnot, Knot toKnot)
    {
        var x = toKnot.X - fromKnot.X;
        var y = toKnot.Y - fromKnot.Y;

        var direction = (x, y) switch
        {
            { x: var _x, y: var _y } when _x < 0 && _y == 0 => Direction.Left,
            { x: var _x, y: var _y } when _x > 0 && _y == 0 => Direction.Right,
            { x: var _x, y: var _y } when _x == 0 && _y < 0 => Direction.Up,
            { x: var _x, y: var _y } when _x == 0 && _y > 0 => Direction.Down,
            { x: var _x, y: var _y } when _x < 0 && _y < 0 => Direction.UpperLeft,
            { x: var _x, y: var _y } when _x < 0 && _y > 0 => Direction.LowerLeft,
            { x: var _x, y: var _y } when _x > 0 && _y < 0 => Direction.UpperRight,
            { x: var _x, y: var _y } when _x > 0 && _y > 0 => Direction.LowerRight,
            _ => Direction.None
        };
        return direction;
    }

    void UpdatePosition(Knot knot, Knot nextKnot, int x, int y)
    {
        if (IsTouching(knot, nextKnot)) return;

        var direction = GetDirection(nextKnot, knot);
        nextKnot.Add(direction);
    }

    class Knot
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public void Add((int X, int Y) direction)
        {
            X += direction.X;
            Y += direction.Y;
        }
    }

    static class Direction
    {
        public static (int X, int Y) None         { get; } = (0, 0);
        public static (int X, int Y) Up         { get; } = (0, -1);
        public static (int X, int Y) Down       { get; } = (0, 1);
        public static (int X, int Y) Left       { get; } = (-1, 0);
        public static (int X, int Y) Right      { get; } = (1, 0);
        public static (int X, int Y) UpperLeft  { get; } = (-1, -1);
        public static (int X, int Y) UpperRight { get; } = (1, -1);
        public static (int X, int Y) LowerLeft  { get; } = (-1, 1);
        public static (int X, int Y) LowerRight { get; } = (1, 1);
    }