namespace AoC.CSharp;

public static class Day09
{
    public static long PartOne(string content) => content.BuildDisk().SortFragmented().GetCheckSum();
    public static long PartTwo(string content) => content.BuildDisk().SortUnfragmented().GetCheckSum();

    private static ReadOnlySpan<int> SortFragmented(this Span<int> disk)
    {
        var storageIndex = disk.IndexOf(-1);

        for (var fileIndex = disk.Length - 1; fileIndex > storageIndex; fileIndex--)
        {
            disk[storageIndex] = disk[fileIndex];
            disk[fileIndex] = -1;
            storageIndex += disk[storageIndex..].IndexOf(-1);
        }
        
        return disk;
    }
    
    private static ReadOnlySpan<int> SortUnfragmented(this Span<int> disk)
    {
        var lastSkippedFileId = -1;
        
        for (var fileId = disk[^1]; fileId > 0; fileId--)
        {
            var start = disk.IndexOf(fileId);
            var end = disk.LastIndexOf(fileId);
            var fileSpan = disk.Slice(start, end - start + 1);

            var freeDiskSpace = disk[..start].GetFreeDiskSpace(fileSpan.Length);
            if (freeDiskSpace.IsEmpty)
            {
                // bookmark this spot to try again next time
                if (lastSkippedFileId == -1)
                    lastSkippedFileId = fileId;

                continue;
            }
            
            fileSpan.CopyTo(freeDiskSpace);
            fileSpan.Fill(-1);
            
            // we haven't finished processing all files in the current pass, so keep going:
            if (fileId != 1)
                continue;

            // if we've finished processing all files, and we didn't skip anything, we're done:
            if (lastSkippedFileId != -1)
                break;

            // otherwise we need another pass, so go back to the last bookmarked file and go again:
            fileId = lastSkippedFileId + 1;
            lastSkippedFileId = -1;
        }
        
        return disk;
    }
    
    private static Span<int> BuildDisk(this string input)
    {
        Span<int> disk = new int[input.Sum(i => i - 48)];

        var index = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var size = input[i] - 48;
            disk.Slice(index, size).Fill(i % 2 == 0 ? i / 2 : -1);
            index += size;
        }

        return disk;
    }
    
    private static long GetCheckSum(this ReadOnlySpan<int> disk)
    {
        long checkSum = 0;
        
        for (var i = 0; i < disk.Length; i++)
            if (disk[i] != -1)
                checkSum += i * disk[i];
        
        return checkSum;
    }

    private static Span<int> GetFreeDiskSpace(this Span<int> disk, int size)
    {
        var start = disk.IndexOf(Enumerable.Repeat(-1, size).ToArray());
        return start == -1 ? Span<int>.Empty : disk.Slice(start, size);
    }
}