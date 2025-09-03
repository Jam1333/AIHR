using Domain.Constants;
using FluentResults;

namespace Domain.Abstractions.Errors;

public sealed class ValidationError : Error
{
    public ValidationError(IEnumerable<string> errors) : base("One or more validation errors occured")
    {
        Metadata.Add(ErrorConstants.StatusMetadataKey, 400);
        Metadata.Add("errors", errors);
    }
}
