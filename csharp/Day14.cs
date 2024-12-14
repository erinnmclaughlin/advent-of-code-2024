namespace AoC.CSharp;

public static class Day14
{
    public static long GetQuadrantScore(this IEnumerable<Robot> robots, int numRows, int numCols)
    {
        var list = robots.ToList();
        var q1 = list.Count(x => x.Position.X < numCols / 2 && x.Position.Y < numRows / 2);
        var q2 = list.Count(x => x.Position.X < numCols / 2 && x.Position.Y > numRows / 2);
        var q3 = list.Count(x => x.Position.X > numCols / 2 && x.Position.Y < numRows / 2);
        var q4 = list.Count(x => x.Position.X > numCols / 2 && x.Position.Y > numRows / 2);

        return q1 * q2 * q3 * q4;
    }
    
    public sealed class Robot
    {
        public (int X, int Y) Position { get; set; }
        public (int dX, int dY) Velocity { get; set; }

        public void Move(int seconds, int numRows, int numCols)
        {
            var x = (Position.X + Velocity.dX * seconds) % numCols;
            var y = (Position.Y + Velocity.dY * seconds) % numRows;

            if (x < 0) x = numCols + x;
            if (y < 0) y = numRows + y;
            
            Position = (x, y);
        }
        
        public static Robot Parse(string input)
        {
            var splitIndex = input.IndexOf(' ');
            var pos = input[2..splitIndex].Split(',');
            var vel = input[(splitIndex + 3)..].Split(',');
            return new Robot
            {
                Position = (int.Parse(pos[0]), int.Parse(pos[1])),
                Velocity = (int.Parse(vel[0]), int.Parse(vel[1]))
            };
        }
    }
}