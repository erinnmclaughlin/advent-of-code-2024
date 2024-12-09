using System.Text;
using AoC.CSharp;
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
        var blocks = GetBlocks(Example).ToList();
        //Print(blocks);
        while (!IsSorted(blocks))
        {
            var storageBlock = blocks.First(x => x.Id == -1);
            var start = blocks.IndexOf(storageBlock);
            var fileBlock = blocks.Last(x => x.Id > 0);
            var end = blocks.LastIndexOf(fileBlock);

            blocks[start] = fileBlock;
            blocks[end] = storageBlock;
            
            //Print(blocks);
        }
        
        output?.WriteLine(GetCheckSum(blocks).ToString());
    }

    [Fact]
    public void PartTwo()
    {
        var blocks = GetBlocks(Example).Select((b, i) => { b.Index = i; return b; }).ToArray();
        Print(blocks);

        var files = blocks
            .Where(x => x.Id > 0)
            .GroupBy(x => x.Id)
            .OrderByDescending(x => x.Key)
            .ToDictionary(x => x.Key, x => x.ToArray());
        
        var storages = blocks.Where(x => x.Id == -1).OrderBy(x => x.Index).ToArray();

        foreach (var (fileId, fileBlocksToMove) in files)
        {
            //output?.WriteLine("Checking {0} block (size: {1})", fileBlock.Id, fileBlock.Size);
            
            var storageBlock = storages
                .OrderBy(x => x.Index)
                .FirstOrDefault(x => x.Size >= fileBlocksToMove.Length && x.Index < fileBlocksToMove.Min(x => x.Index));

            if (storageBlock == null)
            {
                //output?.WriteLine("No match");
                continue;
            }

            var sbi = storageBlock.Index;
            foreach (var ftm in fileBlocksToMove)
            {
                storageBlock = storages.First(x => x.Index == sbi);
                storageBlock.Index = ftm.Index;
                ftm.Index = sbi++;
                ftm.IsMoved = true;
            }
            
            Array.Sort(storages, (b1, b2) => b1.Index.CompareTo(b2.Index));

            for (var i = 0; i < storages.Length; i++)
            {
                storages[i].Size = 1;

                var cursor = storages[i].Index;

                while (cursor > 0)
                {
                    var curr = storages.FirstOrDefault(x => x.Index == cursor);
                    var prev = storages.FirstOrDefault(x => x.Index == cursor - 1);
                    
                    if (curr != null && prev != null && curr.Index - prev.Index == 1)
                        storages[i].Size++;
                    
                    cursor--;
                }

                cursor = storages[i].Index;

                while (cursor + 1 < storages.Length)
                {
                    if ((storages[cursor + 1].Index - storages[cursor].Index) == 1)
                        storages[i].Size++;
                    
                    cursor++;
                }
            }

            blocks = blocks.OrderBy(x => x.Index).ToArray();
            Print(blocks);
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
        var start = blocks.IndexOf(blocks.First(x => x.Id < 0));
        return blocks.Skip(start).All(x => x.Id < 0);
    }

    private static long GetCheckSum(IList<Block> blocks)
    {
        var b = blocks.OrderBy(x => x.Index).ToArray();
        
        long result = 0;
        for (var i = 0; i < b.Length; i++)
        {
            if (b[i].Id > 0)
            {
                result += (i * b[i].Id);
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
                yield return new Block(id, 0, size);
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

    private Span<char> GetSpan(IList<Block> blocks)
    {
        var sb = new StringBuilder();
        foreach (var block in blocks)
        {
            var c = block.Id < 0 ? ".": Convert.ToString(block.Id);
            sb.Append(c);
            //for (var i = 0; i < block.Size; i++)
            //    sb.Append(c);
        }

        return new Span<char>(sb.ToString().ToCharArray());
    }

    private void Print(IList<Block> blocks)
    {
        var sb = new StringBuilder();

        foreach (var block in blocks)
        {
            var c = block.Id < 0 ? ".": Convert.ToString(block.Id);
            sb.Append(c);
            //for (var i = 0; i < block.Size; i++)
            //    sb.Append(c);
        }

        sb.Append($" ({GetCheckSum(blocks)})");
        
        output?.WriteLine(sb.ToString());
    }

    public class Block(int id, int index, int size)
    {
        public int Id { get; } = id;
        public int Index { get; set; } = index;
        public int Size { get; set; } = size;
        public bool IsMoved { get; set; }
    }
}