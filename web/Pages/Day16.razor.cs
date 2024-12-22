using AoC.CSharp.Common;
using Microsoft.AspNetCore.Components;

namespace AoC.Web.Pages;

public partial class Day16 : ComponentBase
{
    [Inject] 
    private HttpClient HttpClient { get; set; } = null!;
    
    private MazeRunnerStateContainer? StateContainer { get; set; }
    private MazeRunnerStateContainer.MazeRunnerState? SelectedSolution { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var text = await HttpClient.GetStringAsync("day16.example.txt");
        var lines = text.Replace("\r", "").Split('\n');

        var maze = new Maze2D(lines.Length, lines[0].Length);
        var initialState = new MazeRunnerStateContainer.MazeRunnerStateSnapshot(Direction.Right, Vector2D.Zero);
        var target = Vector2D.Zero;
        
        for (var y = 0; y < maze.Height; y++)
        for (var x = 0; x < maze.Width; x++)
        {
            var character = lines[y][x];

            if (character == '.')
                continue;

            var position = new Vector2D(x, y);

            switch (character)
            {
                case '#':
                    maze.Walls.Add(position);
                    break;
                case 'S':
                    initialState = initialState with { Position = position };
                    break;
                case 'E':
                    target = position;
                    break;
            }
        }

        StateContainer = new MazeRunnerStateContainer(maze, initialState, target);

        //while (StateContainer.HasNext())
        //{
        //    StateContainer.MoveNext();
        //}
    }

    private void Select(MazeRunnerStateContainer.MazeRunnerState? solution)
    {
        SelectedSolution = SelectedSolution == solution ? null : solution;
    }
}