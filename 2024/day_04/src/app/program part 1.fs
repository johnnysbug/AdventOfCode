let XMAS = "XMAS"
let rows = System.IO.File.ReadAllLines("/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_04/input.txt")

let maxY = rows.Length
let maxX = rows[0].Length

type Direction =
    | Up | Down | Left | Right
    | UpperLeft | LowerLeft | UpperRight | LowerRight

let directional d =
    match d with
    | Up -> (0, -1)             //  y - 1
    | Down -> (0, 1)            //  y + 1
    | Left -> (-1, 0)           //  x - 1
    | Right -> (1, 0)           //  x + 1
    | UpperLeft -> (-1, -1)     //  x - 1, y - 1
    | UpperRight -> (1, -1)     //  x + 1, y - 1
    | LowerLeft -> (-1, 1)      //  x - 1, y + 1
    | LowerRight -> (1, 1)      //  x + 1, y + 1

let countMatches (x, y, d) =
    let (dx, dy) = directional d
    let count = 
        XMAS 
        |> Seq.mapi(fun i c -> if c = rows[y + (dy * i)][x + (dx * i)] then 1 else 0)
        |> Seq.sum
    if count = XMAS.Length then 1 else 0
    
let matches (x, y) =
    let endX = x + (XMAS.Length - 1)
    let endY = y + (XMAS.Length - 1)
    [ Down; Up; Left; Right; UpperLeft; UpperRight; LowerLeft; LowerRight ]
    |> Seq.map (fun d ->
        match d with 
        | Up when y < maxY && y > 2 -> 
            countMatches (x, y, d)
        | Down when y >= 0 && endY < maxY -> 
            countMatches (x, y, d)
        | Left when x < maxX && x > 2 ->
            countMatches (x, y, d)
        | Right when x >= 0 && endX < maxX ->
            countMatches (x, y, d)
        | UpperLeft when y < maxY && y > 2 && x < maxX && x > 2 -> 
            countMatches (x, y, d)
        | UpperRight when y < maxY && y > 2 && x >= 0 && endX < maxX -> 
            countMatches (x, y, d)
        | LowerLeft when y >= 0 && endY < maxY && x < maxX && x > 2 ->
            countMatches (x, y, d)
        | LowerRight when y >= 0 && endY < maxY && x >= 0 && endX < maxX ->
            countMatches (x, y, d)
        | _ -> 0
    )
    |> Seq.sum

let exes = seq {
    for y = 0 to maxY - 1 do
        for x = 0 to maxX - 1 do
            if rows[y][x] = 'X' then yield matches (x, y)
}

System.Console.WriteLine(exes |> Seq.sum)