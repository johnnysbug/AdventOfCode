open System.IO

let calc (dim: string) =
    let nums = dim.Split('x')
    let l = int nums[0]
    let w = int nums[1]
    let h = int nums[2]

    let smallest =
        [| l; w; h; |]
        |> Array.sort
        |> Array.take 2

    (l * w * h) + smallest[0] * 2 + smallest[1] * 2

let data = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/2015/day_02/input.txt"

let total =
    data
    |> Seq.map calc
    |> Seq.sum

printfn $"{total}"