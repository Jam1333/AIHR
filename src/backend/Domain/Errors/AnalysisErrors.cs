using Domain.Abstractions.Errors;
using FluentResults;

namespace Domain.Errors;

public static class AnalysisErrors
{
    public static Error NotFound(Guid id) => new NotFoundError($"Analysis with id='{id}' not found");
    public static readonly Error WrongAuthor = new AccessDeniedError("You are not the author of the analysis");
}
