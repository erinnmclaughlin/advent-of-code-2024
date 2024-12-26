namespace AoC;

public class Day16
{
    [Theory]
    [InlineData("day16.example1.txt", 7036)]
    [InlineData("day16.example2.txt", 11048)]
    [InlineData("day16.txt", 147628)]
    public void PartOne(string filePath, int expected)
    {
        Assert.Equal(expected, CSharp.Day16.PartOne(File.ReadAllLines(filePath)));
    }
    
    [Theory]
    [InlineData("day16.example1.txt", 45)]
    [InlineData("day16.example2.txt", 64)]
    [InlineData("day16.txt", 670)]
    public void PartTwo(string filePath, int expected)
    {
        Assert.Equal(expected, CSharp.Day16.PartTwo(File.ReadAllLines(filePath)));
    }
}