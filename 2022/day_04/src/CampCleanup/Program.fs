open System.IO

let assignments (assignment: string) =
    assignment.Split(',')

let ids (sections: string array) =
    (int sections[0], int sections[sections.Length - 1])

let overlap (sections: string array) =
    let (l1, l2) = ids(sections.[0].Split('-'))
    let (r1, r2) = ids(sections.[1].Split('-'))

    if ((l1 <= r1 && l2 >= r2) || (r1 <= l1 && r2 >= l2)) then
        1 
    elif ((r1 <= l2 && r1 >= l1) || (l1 <= r2 && l1 >= r1)) then
        1
    else
        0

let rows = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/day_04/input.txt"

rows
|> Seq.map assignments
|> Seq.map overlap
|> Seq.sum
|> (fun (s) -> printfn $"{s}")
