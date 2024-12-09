using System.Text;
using Xunit.Abstractions;

namespace AoC;

public class Day09(ITestOutputHelper? output = null)
{
    private readonly string _fileLines = File.ReadAllText("day09.txt");

    private const string Example = "2333133121414131402";
    private const string Expected = "00...111...2...333.44.5555.6666.777.888899";

    [Fact]
    public void PartOne()
    {
        var blocks = GetBlocks(_fileLines).ToList();
        //Print(blocks);
        while (!IsSorted(blocks))
        {
            var storageBlock = blocks.First(x => x.Id == -1);
            var start = blocks.IndexOf(storageBlock);
            var fileBlock = blocks.Last(x => x.Id != -1);
            var end = blocks.LastIndexOf(fileBlock);

            blocks[start] = fileBlock;
            blocks[end] = storageBlock;
            
            //Print(blocks);
        }
        
        output?.WriteLine(GetCheckSum(blocks).ToString());
    }

    [Fact]
    public void IsSortedTest()
    {
        Assert.False(IsSorted(GetBlocks(Example).ToArray()));
    }
    
    private static bool IsSorted(IList<Block> blocks)
    {
        var start = blocks.IndexOf(blocks.First(x => x.Id == -1));
        return blocks.Skip(start).All(x => x.Id == -1);
    }

    private static long GetCheckSum(IList<Block> blocks)
    {
        long result = 0;
        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].Id != -1)
            {
                result += (i * blocks[i].Id);
            }
        }

        return result;
    }
    
    private static IEnumerable<Block> GetBlocks(string input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var id = i % 2 == 0 ? i / 2 : -1;
            var size = int.Parse(input[i].ToString());
            
            for (var s = 0; s < size; s++)
                yield return new Block(id, size);
        }
    }
    
    [Fact]
    public void ExampleParsing()
    {
        var sb = new StringBuilder();
        var isFile = true;

        var id = -1;
        for (var i = 0; i < Example.Length; i++)
        {
            isFile = !isFile;
        }
        
        output?.WriteLine(sb.ToString());
    }

    private void Print(IList<Block> blocks)
    {
        var sb = new StringBuilder();

        foreach (var block in blocks)
        {
            var c = block.Id == -1 ? ".": Convert.ToString(block.Id);
            sb.Append(c);
            //for (var i = 0; i < block.Size; i++)
            //    sb.Append(c);
        }

        sb.Append($" ({GetCheckSum(blocks)})");
        
        output?.WriteLine(sb.ToString());
    }

    public record Block(int Id, int Size);
}