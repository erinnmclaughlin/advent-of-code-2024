namespace AoC.CSharp;

public static class Day09
{
    public static long PartOne(ReadOnlySpan<char> content)
    {
        return Disk.Create(content).SortFragmented().GetCheckSum();
    }

    public static long PartTwo(ReadOnlySpan<char> content)
    {
        return Disk.Create(content).SortUnfragmented().GetCheckSum();
    }
    
    private readonly ref struct Disk(Span<int> memory)
    {
        private readonly Span<int> _memory = memory;
        
        public static Disk Create(ReadOnlySpan<char> input)
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
            
            return new Disk(memory);
        }

        public long GetCheckSum()
        {
            long checkSum = 0;
            for (var i = 0; i < _memory.Length; i++)
            {
                if (_memory[i] != -1)
                    checkSum += (long)i * _memory[i];
            }
            return checkSum;
        }

        public Disk SortFragmented()
        {
            while (_memory.TrimEnd(-1).Contains(-1))
            {
                var fileIndex = _memory.LastIndexOfAnyExcept(-1);
                var storageIndex = _memory.IndexOf(-1);

                _memory[storageIndex] = _memory[fileIndex];
                _memory[fileIndex] = -1;
            }

            return this;
        }

        public Disk SortUnfragmented()
        {
            var fileId = _memory[^1];
            var lastSkippedFileId = -1;
            
            while (true)
            {
                var fileSpan = GetFileSpan(fileId);

                if (!TryGetFreeMemory(fileSpan.Length, out var memory) || _memory.IndexOf(memory) > _memory.IndexOf(fileSpan))
                {
                    if (lastSkippedFileId == -1)
                        lastSkippedFileId = fileId;
                    
                    if (fileId-- == -1)
                        break;
                    
                    continue;
                }
                
                fileSpan.CopyTo(memory);
                fileSpan.Fill(-1);
                
                if (fileId-- != -1)
                    continue;

                if (lastSkippedFileId != -1)
                    break;

                fileId = lastSkippedFileId;
                lastSkippedFileId = -1;
            }
            
            return this;
        }

        private Span<int> GetFileSpan(int fileId)
        {
            var start = _memory.IndexOf(fileId);
            var end = _memory.LastIndexOf(fileId);
            return _memory.Slice(start, end - start + 1);
        }

        private bool TryGetFreeMemory(int size, out Span<int> memory)
        {
            var searchValue = Enumerable.Repeat(-1, size).ToArray();
            var start = _memory.IndexOf(searchValue);

            if (start == -1)
            {
                memory = Span<int>.Empty;
                return false;
            }
            
            memory = _memory.Slice(start, size);
            return true;
        }
    }
}