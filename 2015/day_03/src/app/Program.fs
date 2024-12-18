open System.IO
open System.Collections.Generic

let data = File.ReadAllText @"/Users/jonathanmoosekian/Projects/AdventOfCode/2015/day_02/input.txt"

let test = "^>v<"

type Address = {X:int; Y:int}

let build next =
    

let addresses = new Dictionary<Address, int>()

addresses[{ X = 0; Y = 0; }] <- 1

// addresses[{ X = 1; Y = 1; }] <- addresses[{ X = 1; Y = 1; }] + 1

let v = addresses[{ X = 1; Y = 1; }]



// let calc (test.ToCharArray()) x y =
//     let rec inner acc rest x y =
//         match rest with
//         | [||] -> acc
//         | arr ->
//             let next = Array.head arr
//             let remaining = Array.tail arr
