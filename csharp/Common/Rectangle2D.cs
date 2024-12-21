namespace AoC.CSharp.Common;

public struct Rectangle2D
{
    /// <summary>
    /// The top left position of the rectangle
    /// </summary>
    public Vector2D Position { get; set; }

    public int Height { get; set; } 
    public int Width { get; set; }

    public Rectangle2D(Vector2D? position = null, int height = 1, int width = 1)
    {
        if (height <= 0) throw new ArgumentException("Height must be a positive integer.", nameof(height));
        if (width <= 0) throw new ArgumentException("Width must be a positive integer.", nameof(width));

        Position = position ?? Vector2D.Zero;
        Height = height;
        Width = width;
    }
    
    public bool Contains(int x, int y) => Contains(new Vector2D(x, y));
    
    public bool Contains(Vector2D position) => 
        position.Y >= Position.Y && position.Y < Position.Y + Height &&
        position.X >= Position.X && position.X < Position.X + Width;

    public static Rectangle2D Create(int x, int y, int height, int width) => new(new Vector2D(x, y), height, width);

    public static Rectangle2D Create(Vector2D a, Vector2D b)
    {
        var topLeft = new Vector2D(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
        var height = Math.Max(a.Y, b.Y) - topLeft.Y + 1;
        var width = Math.Max(a.X, b.X) - topLeft.X + 1;
        
        return new Rectangle2D(topLeft, height, width);
    }
}