using Domain.Constants;
using FluentResults;

namespace Domain.Abstractions.Errors;

public sealed class ConflictError : Error
{
    public ConflictError(string message) : base(message) 
    {
        Metadata.Add(ErrorConstants.StatusMetadataKey, 409);
    }
}
