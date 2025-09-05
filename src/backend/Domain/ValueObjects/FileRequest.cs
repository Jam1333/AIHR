namespace Domain.ValueObjects;

public record FileRequest(string FileName, Stream Stream) : IDisposable
{
    public void Dispose()
    {
        Stream.Dispose();
    }
}
