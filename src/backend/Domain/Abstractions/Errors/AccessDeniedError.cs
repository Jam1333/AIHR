using Domain.Constants;
using FluentResults;

namespace Domain.Abstractions.Errors;

public sealed class AccessDeniedError : Error
{
    public AccessDeniedError(string message) : base(message) 
    {
        Metadata.Add(ErrorConstants.StatusMetadataKey, 403);
    }
}
