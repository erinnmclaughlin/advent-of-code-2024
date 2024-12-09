using Xunit.Abstractions;

namespace AoC;

public class Day09a(ITestOutputHelper? output = null)
{
    private const string Example = "2333133121414131402";
    private readonly string _fileContent = File.ReadAllText("day09.txt");

    [Fact]
    public void PartOne()
    {
        var disk = Disk.Create(_fileContent);
        disk.SortFragmented();
        Assert.Equal(6262891638328, disk.GetCheckSum());
    }
    
    [Fact]
    public void Test()
    {
        var disk = Disk.Create(Example);
        Assert.Equal(42, disk.TotalSize);
    }

    [Fact]
    public void TestSortFragmented()
    {
        var disk = Disk.Create(Example);
        disk.SortFragmented();
        
        Assert.Equal("0099811188827773336446555566..............", disk.ToCharSpan().ToString());
        Assert.Equal(1928, disk.GetCheckSum());
    }
    
    public readonly ref struct Disk(int size)
    {
        public int TotalSize { get; } = size;
        public Span<int> Memory { get; } = new int[size];
        
        public static Disk Create(ReadOnlySpan<char> input)
        {
            var totalSize = 0;
            for (var i = 1; i < 10; i++)
                totalSize += i * input.Count($"{i}");
            
            var disk = new Disk(totalSize);

            var index = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var size = int.Parse(input[i].ToString());

                if (size == 0) continue;
                
                disk.Memory.Slice(index, size).Fill(i % 2 == 0 ? i / 2 : -1);
                index += size;
            }
            return disk;
        }

        public long GetCheckSum()
        {
            long checkSum = 0;
            for (var i = 0; i <= Memory.LastIndexOfAnyExcept(-1); i++)
            {
                checkSum += i * Memory[i];
            }
            return checkSum;
        }

        public void SortFragmented()
        {
            while (Memory.TrimEnd(-1).Contains(-1))
            {
                var fileIndex = Memory.LastIndexOfAnyExcept(-1);
                var storageIndex = Memory.IndexOf(-1);

                Memory[storageIndex] = Memory[fileIndex];
                Memory[fileIndex] = -1;
            }
        }

        public ReadOnlySpan<char> ToCharSpan()
        {
            Span<char> charSpan = new char[Memory.Length];
            for (var i = 0; i < Memory.Length; i++)
            {
                var id = Memory[i];
                charSpan[i] = id == -1 ? '.' : Convert.ToChar(id + 48);
            }
            return charSpan;
        }
    }

    public sealed class FileBlock
    {
        public int Id { get; set; }
        public int Size { get; set; } // todo: byte
    }
}