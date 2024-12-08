namespace AoC.CSharp;

using Cell = (int Row, int Col);

public static class Day08Optimized
{
    public static int PartOne(ReadOnlySpan<string> grid)
    {
        var antiNodes = new HashSet<int>();
        
        var (numRows, numCols) = (grid.Length, grid[0].AsSpan().Length);
  
        grid.GetAntennaPairs((c1, c2) =>
        {
            var (dX, dY) = (c2.Col - c1.Col, c2.Row - c1.Row);

            c1 = (c1.Row - dY, c1.Col - dX);
            
            if (c1.IsInBounds(numRows, numCols))
                antiNodes.Add(c1.GetHashCode());

            c2 = (c2.Row + dY, c2.Col + dX);
            
            if (c2.IsInBounds(numRows, numCols))
                antiNodes.Add(c2.GetHashCode());
        });
        
        return antiNodes.Count;
    }
    
    public static int PartTwo(ReadOnlySpan<string> grid)
    {
        var antiNodes = new HashSet<int>();
        var (numRows, numCols) = (grid.Length, grid[0].AsSpan().Length);
  
        GetAntennaPairs(grid, (c1, c2) =>
        {
            var (dX, dY) = (c2.Col - c1.Col, c2.Row - c1.Row);
            
            while (c1.IsInBounds(numRows, numCols))
            {
                antiNodes.Add(c1.GetHashCode());
                c1 = (c1.Row - dY, c1.Col - dX);
            }

            while (c2.IsInBounds(numRows, numCols))
            {
                antiNodes.Add(c2.GetHashCode());
                c2 = (c2.Row + dY, c2.Col + dX);
            }
        });
        
        return antiNodes.Count;
    }

    private static void GetAntennaPairs(this ReadOnlySpan<string> grid, Action<Cell, Cell> handle)
    {
        for (var i = 0; i < grid.Length; i++)
        {
            var rowSpan = grid[i].AsSpan();

            if (!rowSpan.ContainsAnyExcept('.'))
                continue;
            
            for (var j = rowSpan.IndexOfAnyExcept('.'); j < rowSpan.Length; j++)
            {
                var c = rowSpan[j];

                if (c == '.') continue;
                
                for (var k = grid.Length - 1; k > i; k--)
                {
                    var otherRowSpan = grid[k].AsSpan();

                    for (var l = otherRowSpan.IndexOf(c); l >= 0 && l < otherRowSpan.Length; l++)
                        if (otherRowSpan[l] == c)
                            handle((i, j), (k, l));
                }
            }
        }
    }
    
    private static bool IsInBounds(this Cell cell, int maxRows, int maxCols) =>
        cell.Row >= 0 && 
        cell.Row < maxRows &&
        cell.Col >= 0 && 
        cell.Col < maxCols;
}