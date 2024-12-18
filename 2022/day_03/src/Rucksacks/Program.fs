open System.IO

let items = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

let priority (rucksacks: string array) =
    let first = rucksacks.[0]
    let second = rucksacks.[1]
    let third = rucksacks.[2]

    query {
        for f in first do
        join s in second on (f = s)
        join t in third on (s = t)
        select (items.IndexOf(f) + 1)
        head
    }
    
let rucksacks = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_03/input.txt"

rucksacks
|> Seq.chunkBySize 3
|> Seq.map priority
|> Seq.sum
|> (fun (s) -> printfn $"{s}")
