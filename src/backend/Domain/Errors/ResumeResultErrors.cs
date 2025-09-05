using Domain.Abstractions.Errors;
using FluentResults;

namespace Domain.Errors;

public static class ResumeResultErrors
{
    public static Error NotFound(Guid id) => new NotFoundError($"Resume result with id='{id}' not found");
}
