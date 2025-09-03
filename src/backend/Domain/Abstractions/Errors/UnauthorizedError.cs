using Domain.Constants;
using FluentResults;

namespace Domain.Abstractions.Errors;

public sealed class UnauthorizedError : Error
{
    public UnauthorizedError(string message) : base(message) 
    {
        Metadata.Add(ErrorConstants.StatusMetadataKey, 401);
    }
}
