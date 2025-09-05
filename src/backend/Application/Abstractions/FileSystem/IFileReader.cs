using Domain.ValueObjects;

namespace Application.Abstractions.FileSystem;

public interface IFileReader
{
    Task<string> ReadAllTextAsync(FileRequest file);
    Task<FileContent[]> ReadAllTextsFromFilesRangeAsync(IEnumerable<FileRequest> files);
}
