namespace AoC.CSharp;

public static class Day09
{
    public static long PartOne(ReadOnlySpan<char> content) => BuildMemory(content).SortFragmented().GetCheckSum();
    public static long PartTwo(ReadOnlySpan<char> content) => BuildMemory(content).SortUnfragmented().GetCheckSum();

    private static Span<int> BuildMemory(ReadOnlySpan<char> input)
    {
        var totalSize = 0;
        for (var i = 1; i < 10; i++)
            totalSize += i * input.Count($"{i}");

        Span<int> memory = new int[totalSize];

        var index = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var size = int.Parse(input[i].ToString());

            if (size == 0) continue;
                
            memory.Slice(index, size).Fill(i % 2 == 0 ? i / 2 : -1);
            index += size;
        }

        return memory;
    }
    
    private static long GetCheckSum(this Span<int> memory)
    {
        long checkSum = 0;
        for (var i = 0; i < memory.Length; i++)
        {
            if (memory[i] != -1)
                checkSum += (long)i * memory[i];
        }
        return checkSum;
    }
    
    public static Span<int> SortFragmented(this Span<int> memory)
    {
        while (memory.TrimEnd(-1).Contains(-1))
        {
            var fileIndex = memory.LastIndexOfAnyExcept(-1);
            var storageIndex = memory.IndexOf(-1);

            memory[storageIndex] = memory[fileIndex];
            memory[fileIndex] = -1;
        }

        return memory;
    }
    
    private static Span<int> SortUnfragmented(this Span<int> memory)
    {
        var fileId = memory[^1];
        var lastSkippedFileId = -1;
            
        while (true)
        {
            var start = memory.IndexOf(fileId);
            var end = memory.LastIndexOf(fileId);
            var fileSpan = memory.Slice(start, end - start + 1);

            if (!memory.TryGetFreeMemory(fileSpan.Length, out var m) || memory.IndexOf(m) > memory.IndexOf(fileSpan))
            {
                if (lastSkippedFileId == -1)
                    lastSkippedFileId = fileId;
                    
                if (fileId-- == -1)
                    break;
                    
                continue;
            }
                
            fileSpan.CopyTo(m);
            fileSpan.Fill(-1);
                
            if (fileId-- != -1)
                continue;

            if (lastSkippedFileId != -1)
                break;

            fileId = lastSkippedFileId;
            lastSkippedFileId = -1;
        }
            
        return memory;
    }
    
    private static bool TryGetFreeMemory(this Span<int> memory, int size, out Span<int> freeMemory)
    {
        var searchValue = Enumerable.Repeat(-1, size).ToArray();
        var start = memory.IndexOf(searchValue);

        if (start == -1)
        {
            freeMemory = Span<int>.Empty;
            return false;
        }
            
        freeMemory = memory.Slice(start, size);
        return true;
    }
}