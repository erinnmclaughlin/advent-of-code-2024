using System.Collections.Immutable;
using AoC.CSharp.Common;

namespace AoC.Web.Pages;

public partial class Day16
{
    private MazeState? Maze { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InvokeAsync(() =>
            {
                Maze = CreateMaze();
                StateHasChanged();
            });
        }
    }

    private static MazeState CreateMaze()
    {
        var lines = ExampleInput.Contains("\r\n") ? ExampleInput.Split("\r\n") : ExampleInput.Split("\n");

        var (runner, target) = (Vector2D.Zero, Vector2D.Zero);
        var walls = new Dictionary<Vector2D, GridObject>();
        
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y].AsSpan();
            
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '.')
                    continue;

                var position = new Vector2D(x, y);
                
                switch (line[x])
                {
                    case '#': walls.Add(position, new GridObject(position)); break;
                    case 'S': runner = position; break;
                    case 'E': target = position; break;
                }
            }
        }

        return new MazeState
        {
            Height = lines.Length,
            Width = lines[0].Length,
            MazeRunner = new GridObject(runner),
            Target = target,
            Walls = walls.ToImmutableDictionary()
        };
    }
    
    private class MazeState
    {
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required GridObject MazeRunner { get; init; }
        public required Vector2D Target { get; init; }
        public required ImmutableDictionary<Vector2D, GridObject> Walls { get; init; }
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