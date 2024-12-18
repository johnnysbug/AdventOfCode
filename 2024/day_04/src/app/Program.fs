let rows = System.IO.File.ReadAllLines("/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_04/input.txt")

let maxY = rows.Length
let maxX = rows[0].Length
    
let matches (x, y) =
    match (x, y) with 
    | (x, y) when x >= 1 && x < rows[0].Length - 1 && y >= 1 && y < rows.Length - 1 -> 
        let side1 = $"{rows[y - 1][x - 1]}{rows[y][x]}{rows[y + 1][x + 1]}"
        let side2 = $"{rows[y + 1][x - 1]}{rows[y][x]}{rows[y - 1][x + 1]}"
        if ((side1 = "MAS" || side1 = "SAM") && (side2 = "MAS" || side2 = "SAM")) then 1 else 0
    | _ -> 0

let exes = seq {
    for y = 0 to maxY - 1 do
        for x = 0 to maxX - 1 do
            if rows[y][x] = 'A' then yield matches (x, y)
}

System.Console.WriteLine(exes |> Seq.sum)
