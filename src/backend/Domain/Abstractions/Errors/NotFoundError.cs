using Domain.Constants;
using FluentResults;

namespace Domain.Abstractions.Errors;

public sealed class NotFoundError : Error
{
    public NotFoundError(string message) : base(message) 
    {
        Metadata.Add(ErrorConstants.StatusMetadataKey, 404);
    }
}
