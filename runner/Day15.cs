using System.Text;
using Xunit.Abstractions;

namespace AoC;

public class Day15(ITestOutputHelper output)
{
    private readonly string[] _exampleFileLines = File.ReadAllLines("day15.example.txt");
    private readonly string[] _fileLines = File.ReadAllLines("day15.txt");

    [Fact]
    public void PartOne()
    {
        var span = _fileLines.AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = new World();
        
        for (var y = 0; y < splitIndex; y++)
        {
            var line = _fileLines[y].AsSpan();
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

        for (var i = splitIndex; i < _fileLines.Length; i++)
            foreach (var dir in _fileLines[i].Select(ParseInstruction))
                world.TryMove(world.Robot, dir);

        var answer = world
            .GameObjects
            .OfType<Box>()
            .Sum(b => 100 * b.Position.Y + b.Position.X);
        
        Assert.Equal(1438161, answer);
    }

    // 1437004 is too low??
    [Fact]
    public void PartTwo()
    {
        var span = _exampleFileLines.AsSpan();
        var splitIndex = span.IndexOf(string.Empty);
        var world = new World();
        
        for (var y = 0; y < splitIndex; y++)
        {
            var line = span[y].AsSpan();
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                    world.GameObjects.Add(new Wall { Position = new Vector2D(x * 2, y), Width = 2 });

                else if (line[x] == 'O')
                    world.GameObjects.Add(new Box { Position = new Vector2D(x * 2, y), Width = 2 });

                else if (line[x] == '@')
                    world.Robot.Position = new Vector2D(x * 2, y);
            }
        }
        
        //Assert.Equal(20, world.Width);
        //Assert.Equal(10, world.Height);
        
        output.WriteLine(world.ToStringMap());
        
        for (var i = splitIndex; i < span.Length; i++)
            foreach (var dir in span[i].Select(ParseInstruction))
                world.TryMove(world.Robot, dir);

        output.WriteLine(world.ToStringMap());

        var answer = world
            .GameObjects
            .OfType<Box>()
            .Sum(b => 100 * b.Position.Y + b.Position.X);
            /*.Sum(b => 100 
                      * (b.Position.Y < world.Height / 2 ? b.Position.Y : world.Height - b.Position.Y - 1)
                      + (b.Position.X < world.Width / 2 ? b.Position.X : world.Width - b.Width)
            );*/
     
        Assert.Equal(9021, answer);
    }

    private sealed class World
    {
        public int Height => GameObjects.Max(x => x.Position.Y + x.Height);
        public int Width => GameObjects.Max(x => x.Position.X + x.Width);
        
        public HashSet<GameObject> GameObjects { get; set; } = [];
        public Robot Robot { get; set; } = new();

        public bool TryMove(GameObject target, Vector2D dir)
        {
            if (!target.Movable) return false;
            
            target.Position += dir;
            var collidingObjects = GameObjects.Where(x => x.CollidesWith(target));

            foreach (var collidingObject in collidingObjects)
            {
                if (!TryMove(collidingObject, dir))
                {
                    target.Position -= dir;
                    return false;
                }
            }
            
            return true;
        }
        
        public string ToStringMap()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Robot.Position.X == x && Robot.Position.Y == y)
                    {
                        sb.Append('@');
                        continue;
                    }
                    
                    var obj = GameObjects.FirstOrDefault(o => o.CollidesWith(new Vector2D(x, y)));

                    if (obj is Box { Width: 2 })
                    {
                        sb.Append("[]");
                        x++;
                        continue;
                    }
                    
                    sb.Append(obj switch
                    {
                        Wall => '#',
                        Box => 'O',
                        Day15.Robot => '@',
                        _ => '.'
                    });
                }

                foreach (var box in GameObjects.OfType<Box>().Where(x => x.Position.Y == y))
                {
                    //sb.Append($" top: {box.Position.Y};");
                    //sb.Append($" lft: {box.Position.X};");
                    
                    if (box.Position.Y < Height / 2)
                    {
                        sb.Append($" top: {box.Position.Y};");
                    }
                    else
                    {
                        sb.Append($" bot: {Height - box.Position.Y - 1};");
                    }

                    if (box.Position.X < Width / 2)
                    {
                        sb.Append($" lft: {box.Position.X};");
                    }
                    else
                    {
                        sb.Append($" rgt: {Width - box.Position.X - box.Width}");
                    }

                    sb.Append(" //");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
    
    private abstract class GameObject
    {
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public Vector2D Position { get; set; } = Vector2D.Zero;
        public virtual bool Movable { get; set; } = true;

        private IEnumerable<Vector2D> EnumerateCoordinates => Enumerable
            .Range(Position.Y, Height)
            .SelectMany(y => Enumerable.Range(Position.X, Width).Select(x => new Vector2D(x, y)));
        
        public bool CollidesWith(GameObject other) => other != this && EnumerateCoordinates.Any(other.CollidesWith);
        public bool CollidesWith(Vector2D point) => EnumerateCoordinates.Contains(point);
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