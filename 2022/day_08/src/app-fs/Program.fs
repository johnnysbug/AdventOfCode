let data = System.IO.File.ReadAllLines "input.txt"

let scoreToEdge start rowEnd (tree: int) valueCheck increment =
    seq { start .. if increment < 0 then 0 else rowEnd }
    |> Seq.takeWhile (fun i -> valueCheck i >= tree)
    |> Seq.length

let max =
    data
    |> Seq.mapi (fun y r ->
        let row = r.ToCharArray() |> Seq.map (fun c -> int c) |> Seq.toList
        r |> Seq.mapi (fun x tree ->
            let colCheck i =
                let row = data |> Seq.tryItem i
                match row with
                | Some(row) -> int (row.Substring(i, 1))
                | None -> 0

            let rowCheck i = int ( if i < 0 then 0 else row.Item(i))

            let up    = scoreToEdge (y - 1) 0 (int tree) (colCheck) -1
            let down  = scoreToEdge (y + 1) (data.Length - 1) (int tree) (colCheck) -1
            let left  = scoreToEdge (x - 1) 0 (int tree) (rowCheck) -1
            let right = scoreToEdge (x + 1) (row.Length - 1) (int tree) (rowCheck) -1

            up * down * left * right
        ) |> Seq.toList
    )
    |> Seq.concat |> Seq.max
printfn $"{max}"