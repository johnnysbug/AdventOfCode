open System.IO
open System.Text.RegularExpressions

type command =
    | ChangeDirectory
    | List

type args =
    | DirectoryName of name:string
    | MoveOut
    | Root

type commandStatement = { Command : command; Args : args option; }

type Content = {name:string; size:int; isDir:bool}

let getCommand (text: string): commandStatement =
    let pattern = @"(?<prompt>\$).(?<command>\w.)\s(?<arg>\S*)"
    let regex = Regex(pattern, RegexOptions.Compiled)
    let m = regex.Match(text)
    let cmd = m.Groups["command"].Value
    let arg = m.Groups["arg"].Value
    if cmd = "cd" then
        match arg with
        | ".." -> { Command = ChangeDirectory;
                    Args = Some(MoveOut); }
        | "/" ->  { Command = ChangeDirectory;
                    Args = Some(Root); }
        | _ ->    { Command = ChangeDirectory;
                    Args = Some(DirectoryName(arg)) }
    else
        { Command = List;
          Args = None; }

let getContent (text: string) =
    let dirPattern = @"(?<dir>dir)\s(?<directory>.*)"
    let filePattern = @"(?<size>\d+)\s(?<fileName>.*)"
    let dirRegex = Regex(dirPattern, RegexOptions.Compiled)
    let fileRegex = Regex(filePattern, RegexOptions.Compiled)

    if dirRegex.IsMatch(text) then
        let m = dirRegex.Match(text)
        let directory = m.Groups["directory"].Value
        Some({ name = directory; size = 0; isDir = true })
    elif fileRegex.IsMatch(text) then
        let m = fileRegex.Match(text)
        let file = m.Groups["fileName"].Value
        let size = m.Groups["size"].Value
        Some({ name = file; size = int size; isDir = false })
    else
        None

let data = File.ReadAllLines @"/Users/jonathanmoosekian/Projects/AdventOfCode/2022/day_07/input.txt"

let mutable currentDir = Root
let mutable previousDir = Root
let mutable currentContent: Content option = None

for line in data do
    if line.StartsWith("$") then
        let cmd = getCommand line
        match cmd with
        | cmd when cmd.Command = ChangeDirectory ->
            match cmd.Args with
            | Some c when c = Root -> 
                currentDir <- c
                previousDir <- c
            | Some(DirectoryName(name = n)) -> 
                previousDir <- currentDir
                currentDir <- DirectoryName(n)
            | Some c when c = MoveOut ->
                currentDir <- previousDir
                previousDir <- Root
            | _ -> ()
        | cmd when cmd.Command = List -> ()
        | _ -> ()
    else
        let content = getContent line
        match content with
        | Some(content) -> ()
        | _ -> ()
