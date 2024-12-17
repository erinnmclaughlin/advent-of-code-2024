namespace AoC.Web.Pages;

public sealed partial class Day16
{
    private CSharp.Day16.MazeModel? Maze { get; set; }
    private CSharp.Day16.MazeRunner? SelectedRunner { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InvokeAsync(() =>
            {
                var lines = ExampleInput.Contains("\r\n") ? ExampleInput.Split("\r\n") : ExampleInput.Split("\n");
                Maze = CSharp.Day16.MazeModel.Parse(lines);
                StateHasChanged();
            });
        }
    }

    private void Move()
    {
        if (Maze is null) return;

        var runners = Maze.Runners.Where(x => !x.IsDead).ToList();

        foreach (var runner in runners)
        {
            if (runner.Move()) 
                continue;
            
            foreach (var clone in runner.Clones)
                Maze.Runners.Add(clone);

            if (runner.IsDead)
                Maze.Runners.Remove(runner);
        }
    }
    
    private void Select(CSharp.Day16.MazeRunner runner)
    {
        SelectedRunner = SelectedRunner == runner ? null : runner;
    }
    
    private const string ExampleInput = """
    ###############
    #.......#....E#
    #.#.###.#.###.#
    #.....#.#...#.#
    #.###.#####.#.#
    #.#.#.......#.#
    #.#.#####.###.#
    #...........#.#
    ###.#.#####.#.#
    #...#.....#.#.#
    #.#.#.###.#.#.#
    #.....#...#.#.#
    #.###.#.#.#.#.#
    #S..#.....#...#
    ###############
    """;
}