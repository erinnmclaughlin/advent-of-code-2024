namespace AoC.Web.Pages;

public partial class Day15
{
    private CancellationTokenSource? _cts;
    private bool IsLoading { get; set; } = true;
    private CSharp.Day15.World World { get; set; } = new();
    private char[] Instructions { get; set; } = [];
    private int Index { get; set; } = -1;
    private Dictionary<int, HashSet<CSharp.Day15.GameObject>?> MoveResults { get; set; } = [];

    protected override void OnInitialized()
    {
        //var file = await HttpClient.GetStringAsync("day15.txt");
        var lines = ExampleInput.Split(Environment.NewLine).ToArray().AsSpan();

        var splitIndex = -1;
        for (var i = 0; i < lines.Length; i++)
        {
            Console.WriteLine($"{lines[i]} ({lines[i].Length})");

            if (lines[i].Length <= 1)
            {
                splitIndex = i;
                break;
            }
        }
        
        World = CSharp.Day15.CreatePartTwoWorld(lines[..splitIndex]);
        Instructions = lines[(splitIndex + 1)..].ToArray().SelectMany(x => x.ToArray()).ToArray();
        MoveResults = Instructions.Index().Select(x => x.Index)
            .ToDictionary(x => x, HashSet<CSharp.Day15.GameObject>? (_) => null);
        IsLoading = false;
    }

    private async Task AutoPlay()
    {
        if (_cts is not null)
        {
            await _cts.CancelAsync();
            _cts.Dispose();
            _cts = null;
        }

        _cts = new CancellationTokenSource();

        for (var i = Index; i < Instructions.Length - 1; i++)
        {
            GoForward();
            StateHasChanged();

            if (_cts?.IsCancellationRequested ?? true)
                break;

            await Task.Delay(50);

            if (_cts?.IsCancellationRequested ?? true)
                break;
        }

        if (_cts is not null)
        {
            await _cts.CancelAsync();
            _cts.Dispose();
            _cts = null;
        }

        StateHasChanged();
    }

    private void Stop()
    {
        _cts?.Cancel();
    }

    private void GoForward()
    {
        Index++;
        var dir = CSharp.Day15.ParseInstruction(Instructions[Index]);
        var results = new List<CSharp.Day15.GameObject>();
        MoveResults[Index] = World.TryMove(World.Robot, dir, results) ? results.ToHashSet() : [];
    }

    private static string GetGridCss(CSharp.Day15.GameObject obj)
    {
        return GetGridCss(obj.Position, obj.Width);
    }

    private static string GetGridCss(CSharp.Day15.Vector2D pos, int width)
    {
        var (x, y) = (pos.X, pos.Y);
        return $"grid-column-start: {x + 1}; grid-column-end: {x + 1 + width}; grid-row-start: {y + 1}";
    }

    private const string ExampleInput =
"""
##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
""";
}