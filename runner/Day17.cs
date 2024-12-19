using Xunit.Abstractions;

namespace AoC;

public class Day17(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Test1()
    {
        var program = new CSharp.Day17.Program(2,6) { RegisterC = 9 };
        _ = program.Execute();
        Assert.Equal(1, program.RegisterB);
    }

    [Fact]
    public void Test2()
    {
        var program = new CSharp.Day17.Program(5, 0, 5, 1, 5, 4) { RegisterA = 18 };
        Assert.Equivalent(new List<int> { 0,1,2 }, program.Execute());
    }
    
    [Fact]
    public void Test3()
    {
        var program = new CSharp.Day17.Program(0,1,5,4,3,0) { RegisterA = 2024 };
        Assert.Equivalent(new List<int> { 4,2,5,6,7,7,7,7,3,1,0 }, program.Execute());
        Assert.Equal(0, program.RegisterA);
    }
    
    [Fact]
    public void Test4()
    {
        var program = new CSharp.Day17.Program(1,7) { RegisterB = 29 };
        program.Execute();
        Assert.Equal(26, program.RegisterB);
    }

    [Fact]
    public void Example()
    {
        var program = new CSharp.Day17.Program(0, 1, 5, 4, 3, 0) { RegisterA = 729 };
        var output = program.Execute();
        Assert.Equivalent(new List<int>{ 4,6,3,5,6,3,5,2,1,0 }, output);
    }

    [Fact]
    public void PartOne()
    {
        var program = new CSharp.Day17.Program(2,4,1,5,7,5,1,6,0,3,4,0,5,5,3,0) { RegisterA = 24847151 };
        outputHelper.WriteLine(string.Join(',', program.Execute()));
    }
}