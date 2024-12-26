namespace AoC.CSharp;

public static class Day16
{
    public static int PartOne(string[] fileLines) => CreateRunner(fileLines)
        .EnumerateSolutions()
        .First()
        .Cost;

    public static int PartTwo(string[] fileLines) => CreateRunner(fileLines)
        .EnumerateSolutions()
        .SelectMany(x => x.EnumerateVisitedPositions())
        .Distinct()
        .Count();
    
    private static MazeRunner2D CreateRunner(string[] fileLines)
    {
        var maze = new Maze2D(fileLines.Length, fileLines[0].Length);
        var (start, target) = (Vector2D.Zero, Vector2D.Zero);
        
        for (var y = 0; y < maze.Height; y++)
        for (var x = 0; x < maze.Width; x++)
        {
            var character = fileLines[y][x];

            if (character == '.')
                continue;

            var position = new Vector2D(x, y);

            switch (character)
            {
                case '#':
                    maze.Walls.Add(position);
                    break;
                case 'S':
                    start = position;
                    break;
                case 'E':
                    target = position;
                    break;
            }
        }

        return new MazeRunner2D(maze, Direction.Right, start, target);
    }
}