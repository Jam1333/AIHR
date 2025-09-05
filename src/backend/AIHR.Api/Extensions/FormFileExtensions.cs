using Domain.ValueObjects;

namespace AIHR.Api.Extensions;

public static class FormFileExtensions
{
    public static FileRequest ToFileRequest(this IFormFile file)
    {
        return new FileRequest(file.FileName, file.OpenReadStream());
    }
}
