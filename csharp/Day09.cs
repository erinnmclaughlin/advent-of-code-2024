namespace AoC.CSharp;

public static class Day09
{
    private const short FreeSpaceId = -1;
    
    public static long PartOne(string content) => content.BuildDisk().SortFragmented().GetCheckSum();
    public static long PartTwo(string content) => content.BuildDisk().SortUnfragmented().GetCheckSum();

    private static ReadOnlySpan<short> SortFragmented(this Span<short> disk)
    {
        var storageIndex = disk.IndexOf(FreeSpaceId);

        for (var fileIndex = disk.Length - 1; fileIndex > storageIndex; fileIndex--)
        {
            disk[storageIndex] = disk[fileIndex];
            disk[fileIndex] = FreeSpaceId;
            storageIndex += disk[storageIndex..].IndexOf(FreeSpaceId);
        }
        
        return disk;
    }
    
    private static ReadOnlySpan<short> SortUnfragmented(this Span<short> disk)
    {
        short lastSkippedFileId = -1;
        
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
            fileSpan.Fill(FreeSpaceId);
            
            // we haven't finished processing all files in the current pass, so keep going:
            if (fileId != 1) continue;

            // if we've finished processing all files, and we didn't skip anything, we're done:
            if (lastSkippedFileId != -1) break;

            // otherwise we need another pass, so go back to the last bookmarked file and go again:
            fileId = (short)(lastSkippedFileId + 1);
            lastSkippedFileId = -1;
        }
        
        return disk;
    }
    
    private static Span<short> BuildDisk(this string input)
    {
        Span<short> disk = new short[input.Sum(i => i - 48)];

        var index = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var size = input[i] - 48;
            disk.Slice(index, size).Fill((short)(i % 2 == 0 ? i / 2 : FreeSpaceId));
            index += size;
        }
        return disk;
    }
    
    private static long GetCheckSum(this ReadOnlySpan<short> disk)
    {
        long checkSum = 0;
        
        for (var i = 0; i < disk.Length; i++)
            if (disk[i] != FreeSpaceId)
                checkSum += i * disk[i];
        
        return checkSum;
    }

    private static Span<short> GetFreeDiskSpace(this Span<short> disk, int size)
    {
        var start = disk.IndexOf(Enumerable.Repeat(FreeSpaceId, size).ToArray());
        return start == -1 ? Span<short>.Empty : disk.Slice(start, size);
    }
}