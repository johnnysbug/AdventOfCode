open System.IO

let data = File.ReadAllText @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_06/input.txt"

let walk (test: char array) =
    let count = 0
    let rec inner (accumulated: char array) (rest: char array) (count: int) =
        match rest with
        | [||] -> count
        | arr ->
            let head = Array.head arr
            let remaining = Array.tail arr
            if (accumulated.Length = 14) then
                if ((Array.distinct accumulated).Length = 14) then
                    count
                else
                    inner (Array.append (Array.tail accumulated) [| head |]) remaining (count + 1)
            else
                inner (Array.append accumulated [| head |] ) remaining (count + 1)
    inner Array.empty test count

let result =
    walk (data.ToCharArray())

printfn $"{result}"