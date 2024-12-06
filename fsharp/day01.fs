module AoC.fsharp.day01

let parseFile(lines: string array) =
    let left  = Array.zeroCreate lines.Length
    let right = Array.zeroCreate lines.Length
    
    lines |> Array.iteri(fun i l ->
        let parts = l.Split("   ")
        left[i]  <- int parts[0]
        right[i] <- int parts[1]
    )

    (left, right)
    
let part1 (lines: string array) =
    let left, right = (parseFile lines)
    Array.sortInPlace left
    Array.sortInPlace right
    left |> Array.zip right |> Array.sumBy(fun (l, r) -> abs (l - r))

let part2 (lines: string array) =
    let left, right = parseFile lines
    left |> Array.sumBy(fun l -> l * (right |> Array.filter(fun r -> r = l) |> Array.length))
