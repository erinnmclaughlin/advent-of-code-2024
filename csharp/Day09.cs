namespace AoC.CSharp;

public static class Day09
{
    public static long PartOne(ReadOnlySpan<char> content) => BuildMemory(content).SortFragmented().GetCheckSum();
    public static long PartTwo(ReadOnlySpan<char> content) => BuildMemory(content).SortUnfragmented().GetCheckSum();

    private static Span<int> BuildMemory(ReadOnlySpan<char> input)
    {
        var totalSize = 0;
        for (var i = 1; i < 10; i++)
            totalSize += i * input.Count((char)(i + 48));

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
            if (memory[i] != -1)
                checkSum += (long)i * memory[i];
        
        return checkSum;
    }
    
    private static Span<int> SortFragmented(this Span<int> memory)
    {
        var fileIndex = memory.LastIndexOfAnyExcept(-1) + 1;
        var storageIndex = memory.IndexOf(-1);

        while (fileIndex-- > storageIndex)
        {
            memory[storageIndex] = memory[fileIndex];
            memory[fileIndex] = -1;
            storageIndex = memory.IndexOf(-1);
        }
        
        return memory;
    }
    
    private static Span<int> SortUnfragmented(this Span<int> memory)
    {
        var fileId = memory[^1] + 1;
        var lastSkippedFileId = -1;
            
        while (fileId-- > -1)
        {
            var start = memory.IndexOf(fileId);
            var end = memory.LastIndexOf(fileId);
            var fileSpan = memory.Slice(start, end - start + 1);

            if (!memory.TryGetFreeMemory(fileSpan.Length, out var m) || memory.IndexOf(m) > memory.IndexOf(fileSpan))
            {
                // bookmark this spot to try again next time
                if (lastSkippedFileId == -1)
                    lastSkippedFileId = fileId;

                continue;
            }
            
            fileSpan.CopyTo(m);
            fileSpan.Fill(-1);
            
            // we haven't finished processing all files in the current pass, so keep going:
            if (fileId != 0)
                continue;

            // if we've finished processing all files, and we didn't skip anything, we're done:
            if (lastSkippedFileId != -1)
                break;

            // otherwise we need another pass, so go back to the last bookmarked file and go again:
            fileId = lastSkippedFileId + 1;
            lastSkippedFileId = -1;
        }
        
        return memory;
    }
    
    private static bool TryGetFreeMemory(this Span<int> memory, int size, out Span<int> freeMemory)
    {
        var start = memory.IndexOf(Enumerable.Repeat(-1, size).ToArray());

        if (start == -1)
        {
            freeMemory = Span<int>.Empty;
            return false;
        }
            
        freeMemory = memory.Slice(start, size);
        return true;
    }
}