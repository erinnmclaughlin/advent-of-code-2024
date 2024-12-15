namespace AoC;

public class Day15
{
    private readonly string[] _fileLines = File.ReadAllLines("day15.txt");

    [Fact]
    public void PartOne()
    {
        var span = _fileLines.AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = World.Parse(span[..splitIndex]);
        var instructions = span[(splitIndex + 1)..];

        foreach (var line in instructions)
        foreach (var dir in line.Select(ParseInstruction))
            world.TryMove(world.Robot, dir);

        var answer = world
            .GameObjects
            .OfType<Box>()
            .Sum(b => 100 * b.Position.Y + b.Position.X);
        
        Assert.Equal(10092, answer);
    }
    
    private sealed class World
    {
        public HashSet<GameObject> GameObjects { get; set; } = [];
        public Robot Robot { get; set; } = new();

        public bool TryMove(GameObject target, Vector2D dir)
        {
            var collidingObject = GameObjects.FirstOrDefault(x => x.Position == target.Position + dir);

            if (collidingObject is null || collidingObject.Movable && TryMove(collidingObject, dir))
            {
                target.Position += dir;
                return true;
            }

            return false;
        }
        
        public static World Parse(ReadOnlySpan<string> map)
        {
            var world = new World();
            
            for (var y = 0; y < map.Length; y++)
            {
                var line = map[y].AsSpan();
                for (var x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        world.GameObjects.Add(new Wall { Position = new Vector2D(x, y) });
                    if (line[x] == 'O')
                        world.GameObjects.Add(new Box { Position = new Vector2D(x, y) });
                    if (line[x] == '@')
                        world.Robot.Position = new Vector2D(x, y);
                }
            }

            return world;
        }
    }
    
    private abstract class GameObject
    {
        public Vector2D Position { get; set; } = Vector2D.Zero;
        public virtual bool Movable { get; set; } = true;
    }

    private class Box : GameObject;
    private class Robot : GameObject;
    private class Wall : GameObject
    {
        public override bool Movable { get; set; } = false;
    }
    
    private sealed record Vector2D(int X, int Y)
    {
        public static Vector2D Zero => new(0, 0);
        public static Vector2D Up => new(0, -1);
        public static Vector2D Down => new(0, 1);
        public static Vector2D Left => new(-1, 0);
        public static Vector2D Right => new(1, 0);
        
        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
    }
    
    private static Vector2D ParseInstruction(char c) => c switch
    {
        '>' => Vector2D.Right,
        '<' => Vector2D.Left,
        '^' => Vector2D.Up,
        'v' => Vector2D.Down,
        _ => throw new Exception($"'{c}' is not a recognized instruction.")
    };
}