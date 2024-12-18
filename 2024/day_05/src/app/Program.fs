open System.IO

let file = "/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_05/input.txt"
let rules = 
    File.ReadLines(file) |> Seq.filter(fun l -> l.Contains('|')) |> Seq.toList

let pageUpdates = 
    File.ReadLines(file) |> Seq.filter(fun l -> l.Contains(',')) |> Seq.toList

let correctPosition (index, page, rules: seq<int * int>, pages) =
    let afters = rules |> Seq.filter (fun (l, _) -> l = page) |> Seq.map (fun (_, r) -> r) |> Seq.toList
    let befores = rules |> Seq.filter (fun (_, r) -> r = page) |> Seq.map (fun (l, _) -> l) |> Seq.toList

    befores |> List.length = index && 
    pages 
        |> Seq.except [page] 
        |> Seq.forall (fun p -> 
            afters |> Seq.contains p || 
            befores |> Seq.contains p)

let filteredRules update =
    rules
    |> Seq.filter (fun rule -> 
        rule.Split('|')
        |> Seq.forall (fun r -> 
            update |> Seq.exists (fun u -> u = (r |> int))))
    |> Seq.map (fun r -> 
        let parts = r.Split('|')
        (parts[0] |> int, parts[1] |> int))

let correct =
    pageUpdates
    |> Seq.filter(fun u -> 
        let pages = u.Split(',') |> Seq.map int |> Seq.toList
        let rules = filteredRules pages |> Seq.toList
        System.Console.WriteLine($"Filtered rule count for update [{u}]: {rules.Length}")
        pages |> Seq.indexed |> Seq.forall (fun (i, p) -> correctPosition (i, p, rules, pages)))
    |> Seq.map(fun u ->
        let pages = u.Split(',') |> Seq.map int |> Seq.toList
        let index = 
            System.Math.Floor((pages.Length / 2) |> double) |> int
        pages[index])

System.Console.WriteLine($"Rule count: {rules.Length}")
System.Console.WriteLine(correct |> Seq.sum)