using Application.Models;

namespace Application.Abstractions.FileSystem;

public interface IFileReader
{
    Task<string> ReadAllTextAsync(FileRequest file);
}
