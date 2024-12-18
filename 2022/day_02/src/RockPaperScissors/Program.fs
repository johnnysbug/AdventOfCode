open System.IO
open System

// A = Rock = 1
// B = Paper = 2
// C = Scissors = 3

// X = Lose
// Y = Draw
// Z = Win

let shapeScore shape =
    match shape with
    | "A" | "X" -> 1
    | "B" | "Y" -> 2
    | "C" | "Z" -> 3
    | _ -> 0

let lose shape =
    match shape with
    | "A" -> "C"
    | "B" -> "A"
    | "C" -> "B"
    | _ -> shape

let win shape =
    match shape with
    | "A" -> "B"
    | "B" -> "C"
    | "C" -> "A"
    | _ -> shape
    
let shapeToPlay shape strategy =
    match strategy with
    | "X" -> lose shape
    | "Z" -> win shape
    | _ -> shape

let scoreRound (round: string) =
    let plays = round.Split(" ", StringSplitOptions.None)
    let left = shapeScore plays.[0]
    let shapeToPlay = shapeToPlay plays.[0] plays.[1]
    let right = shapeScore shapeToPlay

    match (left, right) with
    | (1, 3) | (3, 2) | (2, 1) -> right
    | (3, 1) | (2, 3) | (1, 2) -> (right + 6)
    | (1, 1) | (2, 2) | (3, 3) -> (right + 3)
    | _ -> 0

let rounds = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_02/input.txt"

rounds
|> Seq.map scoreRound
|> Seq.sum
|> (fun (s) -> printfn $"{s}")
