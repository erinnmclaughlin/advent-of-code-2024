using Xunit.Abstractions;

using Coord = (long X, long Y);

namespace AoC;

public class Day13(ITestOutputHelper output)
{
    private readonly string[] _fileLines = File.ReadAllLines("day13.txt");

    [Fact]
    public void PartOne()
    {
        long sum = 0;
        
        for (var i = 0; i < _fileLines.Length; i+=4)
        {
            // Button A: X+94, Y+34
            var bAText = _fileLines[i].Split('+');
            var bA = new Coord(int.Parse(bAText[1].Split(',')[0]), int.Parse(bAText[2]));
            var bBText = _fileLines[i + 1].Split('+');
            var bB = new Coord(int.Parse(bBText[1].Split(',')[0]), int.Parse(bBText[2]));
        
            // Prize: X=8400, Y=5400
            var prizeText = _fileLines[i + 2].Replace("Prize: X=", "").Replace(" Y=", "").Split(',');
            var target = new Coord(int.Parse(prizeText[0]), int.Parse(prizeText[1]));

            var results = new List<(long NumPressA, long NumPressB)>
            {
                GetPresses(target, bA, bB),
                GetPresses(target, bB, bA, isReversed: true)
            };

            results.RemoveAll(x => x.NumPressA * bA.Y + x.NumPressB * bB.Y != target.Y);
            results.RemoveAll(x => x.NumPressA > 100 || x.NumPressB > 100);
            results = results.OrderBy(x => x.NumPressA).ToList();
            
            if (results.Count > 0)
            {
                var result = results[0];
                var cost = 3 * result.NumPressA + result.NumPressB;
                sum += cost;
                //output.WriteLine("{0} = {1}", result, cost);
            }
            
        }
        
        output.WriteLine(sum.ToString());
    }

    [Fact]
    public void PartTwo()
    {
        long sum = 0;
        
        for (var i = 0; i < _fileLines.Length; i+=4)
        {
            // Button A: X+94, Y+34
            var bAText = _fileLines[i].Split('+');
            var bA = new Coord(int.Parse(bAText[1].Split(',')[0]), int.Parse(bAText[2]));
            var bBText = _fileLines[i + 1].Split('+');
            var bB = new Coord(int.Parse(bBText[1].Split(',')[0]), int.Parse(bBText[2]));
        
            // Prize: X=8400, Y=5400
            var prizeText = _fileLines[i + 2].Replace("Prize: X=", "").Replace(" Y=", "").Split(',');
            var target = new Coord(int.Parse(prizeText[0]) + 10000000000000, int.Parse(prizeText[1]) + 10000000000000);

            var results = new List<(long NumPressA, long NumPressB)>
            {
                GetPresses(target, bA, bB),
                GetPresses(target, bB, bA, isReversed: true)
            };

            results.RemoveAll(x => x.NumPressA * bA.Y + x.NumPressB * bB.Y != target.Y);
            //results.RemoveAll(x => x.NumPressA > 100 || x.NumPressB > 100);
            results = results.OrderBy(x => x.NumPressA).ToList();
            
            if (results.Count > 0)
            {
                var result = results[0];
                var cost = 3 * result.NumPressA + result.NumPressB;
                sum += cost;
                //output.WriteLine("{0} = {1}", result, cost);
            }
            
        }
        
        output.WriteLine(sum.ToString());
    }
    
    private static (long numPressA, long numPressB) GetPresses(Coord target, Coord bA, Coord bB, bool isReversed = false)
    {
        long n1 = bA.Y * target.X;
        long n2 = bA.X * target.Y;

        var numPressB = (n1 - n2) / ((bA.Y * bB.X) - (bA.X * bB.Y));

        var remainingX = target.X - numPressB * bB.X;
        var numPressA = remainingX / bA.X;

        return isReversed ? (numPressB, numPressB) : (numPressA, numPressB);
    }
}