let input = System.IO.File.ReadAllLines("/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_01/input.txt")

let splitter (s: string) =
    let splitted = s.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)
    splitted
let mapper (a: string array) =
    (a[0], a[1])

let splitInput = 
    input
        |> Array.map(splitter)
        |> Array.map(mapper)

let leftSide =
    splitInput
        |> Array.map(fun (l, _) -> l)

let rightSide =
    splitInput
        |> Array.map(fun (_, r) -> r)

let sum = 
    leftSide 
        |> Array.map(fun x -> 
            let count = 
                rightSide 
                |> Seq.filter(fun v -> v = x) 
                |> Seq.length
            (x |> int) * count)
        |> Array.sum

System.Console.WriteLine(sum)