using System.Text;

namespace AoC.CSharp;

public static class Day15
{
    public static World CreatePartOneWorld(ReadOnlySpan<string> input)
    {
        var world = new World();
        
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y].AsSpan();
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

    public static World CreatePartTwoWorld(ReadOnlySpan<string> input)
    {
        var world = new World();
        
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y].AsSpan();
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

        return world;
    }
    
    public sealed class World
    {
        public HashSet<GameObject> GameObjects { get; set; } = [];
        public Robot Robot { get; set; } = new();

        public bool TryMove(GameObject target, Vector2D dir) => TryMove(target, dir, []);
        
        public bool TryMove(GameObject target, Vector2D dir, List<GameObject> affected)
        {
            if (!target.Movable) return false;
            
            target.Position += dir;
            affected.Add(target);
            
            var collidingObjects = GameObjects.Where(x => x.CollidesWith(target)).ToList();

            var canMove = true;
            
            foreach (var collidingObject in collidingObjects)
            {
                if (!TryMove(collidingObject, dir, affected))
                {
                    canMove = false;
                    break;
                }
            }

            if (!canMove)
            {
                for (var i = affected.Count - 1; i >= 0; i--)
                {
                    affected[i].Position -= dir;
                    affected.RemoveAt(i);
                }
            }
            
            return canMove;
        }

        public long GetGpsLocation(GameObject target) => 100 * target.Position.Y + target.Position.X;
    }
    
    public abstract class GameObject
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

    public class Box : GameObject;
    public class Robot : GameObject;
    public class Wall : GameObject
    {
        public override bool Movable { get; set; } = false;
    }
    
    public sealed record Vector2D(int X, int Y)
    {
        public static Vector2D Zero => new(0, 0);
        public static Vector2D Up => new(0, -1);
        public static Vector2D Down => new(0, 1);
        public static Vector2D Left => new(-1, 0);
        public static Vector2D Right => new(1, 0);
        
        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
    }
    
    public static Vector2D ParseInstruction(char c) => c switch
    {
        '>' => Vector2D.Right,
        '<' => Vector2D.Left,
        '^' => Vector2D.Up,
        'v' => Vector2D.Down,
        _ => throw new Exception($"'{c}' is not a recognized instruction.")
    };
    
    public sealed class World2
    {
        public int Height => GameObjects.Max(x => x.Position.Y + x.Height);
        public int Width => GameObjects.Max(x => x.Position.X + x.Width);
        
        public HashSet<GameObject2> GameObjects { get; set; } = [];
        public Robot2 Robot { get; set; } = new();

        public void Move(GameObject2 target, Vector2D2 dir)
        {
            var collidingObjects = GameObjects.Where(x => x != target && x.CollidesWith(target.Position + dir)).ToList();

            foreach (var collidingObject in collidingObjects)
            {
                Move(collidingObject, dir);
            }
            
            target.Position += dir;
        }

        public bool CanMove(GameObject2 target, Vector2D2 dir)
        {
            if (!target.Movable) 
                return false;
            
            var collidingObjects = GameObjects.Where(x => x != target && x.CollidesWith(target.Position + dir)).ToList();
            
            foreach (var collidingObject in collidingObjects)
            {
                if (!CanMove(collidingObject, dir))
                {
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
                    
                    var obj = GameObjects.FirstOrDefault(o => o.CollidesWith(new Vector2D2(x, y)));

                    if (obj is Box2 { Width: 2 })
                    {
                        sb.Append("[]");
                        x++;
                        continue;
                    }
                    
                    sb.Append(obj switch
                    {
                        Wall2 => '#',
                        Box2 => 'O',
                        Day15.Robot2 => '@',
                        _ => '.'
                    });
                }

                if (y < Height / 2)
                {
                    sb.Append($" top: {y};");
                }
                else
                {
                    sb.Append($" bot: {Height - y - 1};");
                }

                var boxes = GameObjects.OfType<Box2>().Where(x => x.Position.Y == y).OrderBy(x => x.Position.X).ToList();
                foreach (var box in boxes)
                {
                    //sb.Append($" top: {box.Position.Y};");
                    //sb.Append($" lft: {box.Position.X};");
                    
                    if (box.Position.X < Width / 2)
                    {
                        sb.Append($" lft: {box.Position.X};");
                    }
                    else
                    {
                        sb.Append($" rgt: {Width - box.Position.X - box.Width}");
                    }

                    if (box != boxes.Last())
                        sb.Append(" //");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
    
    public abstract class GameObject2
    {
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
        public Vector2D2 Position { get; set; } = Vector2D2.Zero;
        public virtual bool Movable { get; set; } = true;

        private IEnumerable<Vector2D2> EnumerateCoordinates => Enumerable
            .Range(Position.Y, Height)
            .SelectMany(y => Enumerable.Range(Position.X, Width).Select(x => new Vector2D2(x, y)));
        
        public bool CollidesWith(GameObject2 other) => other != this && EnumerateCoordinates.Any(other.CollidesWith);
        public bool CollidesWith(Vector2D2 point) => EnumerateCoordinates.Any(p => p.X == point.X && p.Y == point.Y);
    }

    public class Box2 : GameObject2;
    public class Robot2 : GameObject2;
    public class Wall2 : GameObject2
    {
        public override bool Movable { get; set; } = false;
    }
    
    public sealed record Vector2D2(int X, int Y)
    {
        public static Vector2D2 Zero => new(0, 0);
        public static Vector2D2 Up => new(0, -1);
        public static Vector2D2 Down => new(0, 1);
        public static Vector2D2 Left => new(-1, 0);
        public static Vector2D2 Right => new(1, 0);
        
        public static Vector2D2 operator +(Vector2D2 a, Vector2D2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D2 operator -(Vector2D2 a, Vector2D2 b) => new(a.X - b.X, a.Y - b.Y);

        public Vector2D2 Reverse => new Vector2D2(-1 * X, -1 * Y);
    }
    
    public static Vector2D2 ParseInstruction2(char c) => c switch
    {
        '>' => Vector2D2.Right,
        '<' => Vector2D2.Left,
        '^' => Vector2D2.Up,
        'v' => Vector2D2.Down,
        _ => throw new Exception($"'{c}' is not a recognized instruction.")
    };
}