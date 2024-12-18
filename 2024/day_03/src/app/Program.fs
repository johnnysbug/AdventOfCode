open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let matches = Regex.Matches(input, pattern)
    if matches.Count > 0 then Some(matches)
    else None
let exp = @"(?:do\(\))|(?:don\'t\(\))|(?:mul\((\d{1,3})\,(\d{1,3})\))"

let memory = System.IO.File.ReadAllText("/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_03/input.txt")

let processMatch (m: Match): int64 =
    if m.Success then
        let x = m.Groups[1].Value |> int
        let y = m.Groups[2].Value |> int
        x * y |> int64
    else
        0
        
let processMatches (matches: MatchCollection) =
    let _matches = matches |> Seq.sortBy(fun m -> m.Index) |> Seq.toList

    let rec inner (ms: list<Match>, _do: bool, acc: int64) =
        match ms with
        | m :: ms when m.Success && m.Value = "don't()" ->
            inner (ms, false, acc)
        | m :: ms when m.Success && m.Value = "do()" ->
            inner (ms, true, acc)
        | m :: ms when m.Success && _do ->
            let processed = processMatch m
            inner (ms, true, acc + processed)
        | m :: ms when m.Success && _do = false ->
            inner (ms, false, acc)
        | _ -> acc |> int64

    inner (_matches, true, 0)

let total = 
    match memory with 
    | Regex exp matches ->
        processMatches matches
    | _ -> 0

System.Console.WriteLine(total)