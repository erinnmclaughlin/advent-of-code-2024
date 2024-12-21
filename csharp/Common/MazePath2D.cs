namespace AoC.CSharp.Common;

public readonly struct MazePath2D
{
    public Vector2D PointA { get; private init; }
    public Vector2D PointB { get; private init; }
    public int Length { get; private init; }

    public bool Contains(Vector2D point)
    {
        if (PointA.X == PointB.X)
        {
            return point.X == PointA.X &&
                   point.Y >= Math.Min(PointA.Y, PointB.Y) &&
                   point.Y <= Math.Max(PointA.Y, PointB.Y);
        }
        
        return point.Y == PointA.Y &&
               point.X >= Math.Min(PointA.X, PointB.X) &&
               point.X <= Math.Max(PointA.X, PointB.X);
    }
    
    public static MazePath2D Create(Vector2D a, Vector2D b)
    {
        if (a == b || (a.X != b.X && a.Y != b.Y))
            throw new InvalidOperationException("The provided points do not form a horizontal or vertical line.");

        return new MazePath2D
        {
            PointA = a,
            PointB = b,
            Length = Math.Abs(a.X == b.X ? a.Y - b.Y : a.X - b.X)
        };
    }

    public static MazePath2D Create(int aX, int aY, int bX, int bY)
        => Create(new Vector2D(aX, aY), new Vector2D(bX, bY));
}