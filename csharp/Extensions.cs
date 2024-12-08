namespace AoC.CSharp;

public static class Extensions
{
    public static bool Any<T>(this IEnumerable<T> source, Func<T, int, bool> criteria) => source
        .Select((item, index) => (item, index))
        .Any(r => criteria(r.item, r.index));
    
    public static bool IsValidPosition<T>(this T[][] grid, (int Row, int Col) pos) => 
        pos.Row >= 0 &&
        pos.Row < grid.Length &&
        pos.Col >= 0 &&
        pos.Col < grid[0].Length;
}
