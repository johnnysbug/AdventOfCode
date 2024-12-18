let reports = System.IO.File.ReadAllLines("/Users/jonathanmoosekian/Projects/AdventOfCode/2024/day_02/input.txt")

let rec isSafe (values: list<int * int>, increasing: bool option) =
    match values with
    | (l, r) :: vs when System.Math.Abs(l - r) < 4 && System.Math.Abs(l - r) > 0 ->
        if increasing.IsSome && increasing.Value = (l < r) then
            isSafe (vs, increasing)
        elif increasing.IsNone then
            isSafe (vs, Some (l < r))
        else
            false
    | [] -> true
    | _ -> false

let processReport (report: string) =
    let index = 0

    let rec inner (report: string, shrunk: string, index: int) =
        let values = 
            shrunk.Split(" ") 
            |> Seq.map(fun v -> v |> int) 
            |> Seq.pairwise 
            |> Seq.toList

        let isSafe = isSafe(values, None)

        if not isSafe then
            let splitted = 
                report.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)
                |> Array.map(fun v -> v |> int)
                |> Array.toList

            if splitted.Length = index then
                isSafe
            else
                let shortened = 
                    splitted 
                    |> List.removeAt index
                    |> List.map string
                    |> String.concat " "

                inner(report, shortened, index + 1)
        else
            isSafe
    
    inner(report, report, index)

let totalSafe =
    reports
        |> Seq.map(processReport)
        |> Seq.filter(fun s -> s)
        |> Seq.toList

System.Console.WriteLine(totalSafe.Length)