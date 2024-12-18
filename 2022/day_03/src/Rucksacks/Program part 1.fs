open System.IO

let items = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

let priority (rucksack: string) =
    let left = rucksack.Substring(0, rucksack.Length / 2)
    let right = rucksack.Substring (rucksack.Length / 2)
    let index = left.IndexOfAny(right.ToCharArray())
    if (index = -1) then
        0
    else
        (items.IndexOf(left.[index]) + 1)

let rucksacks = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_03/input.txt"

rucksacks
|> Seq.map priority
|> Seq.sum
|> (fun (s) -> printfn $"{s}")
