namespace AoC.CSharp;

public static class Extensions
{
    public static bool Any<T>(this IEnumerable<T> source, Func<T, int, bool> criteria) => source
        .Select((item, index) => (item, index))
        .Any(r => criteria(r.item, r.index));

}
