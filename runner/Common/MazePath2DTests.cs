using AoC.CSharp.Common;
using FluentAssertions;

namespace AoC.Common;

public class MazePath2DTests
{
    [Theory]
    [InlineData(+0, +0, +0, +1, +1)]
    [InlineData(+0, +0, +0, -1, +1)]
    [InlineData(+6, +5, +6, +2, +3)]
    [InlineData(+0, +0, +1, +0, +1)]
    [InlineData(+0, +0, -1, +0, +1)]
    [InlineData(+6, +5, +2, +5, +4)]
    public void CanCreateValidPath(int aX, int aY, int bX, int bY, int expectedLength)
    {
        var pointA = new Vector2D(aX, aY);
        var pointB = new Vector2D(bX, bY);
        var path = MazePath2D.Create(pointA, pointB);
        
        path.PointA.Should().Be(pointA);
        path.PointB.Should().Be(pointB);
        path.Length.Should().Be(expectedLength);
    }

    [Theory]
    [InlineData(+1, +1, false, false)]
    [InlineData(-1, -1, false, false)]
    [InlineData(+0, +0, true, true)]
    [InlineData(+0, +1, true, false)]
    [InlineData(+0, -1, true, false)]
    [InlineData(+0, +5, true, false)]
    [InlineData(+0, -5, true, false)]
    [InlineData(-1, +0, false, true)]
    [InlineData(-5, +0, false, true)]
    [InlineData(+5, +0, false, true)]
    public void ContainsReturnsExpected(int x, int y, bool expectedVertical, bool expectedHorizontal)
    {
        var testVerticalLine = MazePath2D.Create(0, -5, 0, 5);
        var testHorizontalLine = MazePath2D.Create(-5, 0, 5, 0);

        var position = new Vector2D(x, y);
        testVerticalLine.Contains(position).Should().Be(expectedVertical);
        testHorizontalLine.Contains(position).Should().Be(expectedHorizontal);
    }
}