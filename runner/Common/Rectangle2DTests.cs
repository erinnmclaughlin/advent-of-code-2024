using AoC.CSharp.Common;

namespace AoC.Common;

public sealed class Rectangle2DTests
{
    [Theory]
    [InlineData(-5, -5, true)]
    [InlineData(-5, +5, true)]
    [InlineData(+5, +5, true)]
    [InlineData(+5, -5, true)]
    [InlineData(+0, +0, true)]
    [InlineData(+3, +4, true)]
    [InlineData(-5, -6, false)]
    [InlineData(-5, +6, false)]
    [InlineData(+6, -5, false)]
    [InlineData(+6, +5, false)]
    [InlineData(+6, +6, false)]
    [InlineData(-6, -6, false)]
    public void ContainsReturnsExpected(int x, int y, bool expected)
    {
        var rect = Rectangle2D.Create(-5, -5, 11, 11);
        rect.Contains(x, y).Should().Be(expected);
    }

    [Fact]
    public void CreateMethodsAreEquivalent()
    {
        var rect1 = new Rectangle2D(new Vector2D(-5, -5), 11, 11);
        var rect2 = Rectangle2D.Create(-5, -5, 11, 11);
        var rect3 = Rectangle2D.Create(new Vector2D(-5, -5), new Vector2D(5, 5));
        
        rect1.Should().BeEquivalentTo(rect2).And.BeEquivalentTo(rect3);
    }
}