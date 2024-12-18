open System.IO

let data = File.ReadAllText @"/Users/jonathanmoosekian/Projects/AdventOfCode/2015/day_01/input.txt"

let mutable acc = 0

let convertInput i d =
    let result = 
        if (d = '(') then 
            1 
        elif (d = ')') then 
            -1 
        else 
            0
    acc <- acc + result

    if (acc = -1) then
        i + 1
    else
        0

let result = 
    data.ToCharArray()
    |> Seq.mapi convertInput
    |> Seq.toList
