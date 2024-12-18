open System.IO

(*
        [J]         [B]     [T]    
        [M] [L]     [Q] [L] [R]    
        [G] [Q]     [W] [S] [B] [L]
[D]     [D] [T]     [M] [G] [V] [P]
[T]     [N] [N] [N] [D] [J] [G] [N]
[W] [H] [H] [S] [C] [N] [R] [W] [D]
[N] [P] [P] [W] [H] [H] [B] [N] [G]
[L] [C] [W] [C] [P] [T] [M] [Z] [W]
 1   2   3   4   5   6   7   8   9 
*)

let mutable stacks = [|
    [| 'D'; 'T'; 'W'; 'N'; 'L'; |]
    [| 'H'; 'P'; 'C'; |]
    [| 'J'; 'M'; 'G'; 'D'; 'N'; 'H'; 'P'; 'W'; |]
    [| 'L'; 'Q'; 'T'; 'N'; 'S'; 'W'; 'C'; |]
    [| 'N'; 'C'; 'H'; 'P'; |]
    [| 'B'; 'Q'; 'W'; 'M'; 'D'; 'N'; 'H'; 'T'; |]
    [| 'L'; 'S'; 'G'; 'J'; 'R'; 'B'; 'M'; |]
    [| 'T'; 'R'; 'B'; 'V'; 'G'; 'W'; 'N'; 'Z'; |]
    [| 'L'; 'P'; 'N'; 'D'; 'G'; 'W'; |]
|]

let instructions (row: string) =
    let words = row.Split(' ')
    (int words[1], int words[3], int words[5])

let processInstruction ((amount, fromStack, toStack)) =
    let mutable fs = stacks[fromStack - 1]
    let mutable ts = stacks[toStack - 1]

    let fromToMove = Array.take amount fs
    ts <- Array.insertManyAt 0 fromToMove ts
    fs <- Array.removeManyAt 0 amount fs

    stacks[fromStack - 1] <- fs
    stacks[toStack - 1] <- ts
    
let rows = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_05/input.txt"

rows
|> Seq.map instructions
|> Seq.map processInstruction
|> Seq.toList
|> ignore

for x in 0 .. stacks.Length - 1 do
    let top = stacks[x][0]
    printf $"{top}"
