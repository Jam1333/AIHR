using Domain.Abstractions.Errors;
using FluentResults;

namespace Domain.Errors;

public static class InterviewErrors
{
    public static Error NotFound(Guid id) => new NotFoundError($"Interview with id='{id}' not found");
    public static readonly Error WrongAuthor = new AccessDeniedError("You are not the author of the interview");
    public static readonly Error AlreadyEnded = new ConflictError("Interview is already ended");
    public static readonly Error NotEnded = new ConflictError("Interview is not ended yet");
    public static readonly Error HasResult = new ConflictError("Interview has the result already");
    public static readonly Error HasConclusion = new ConflictError("Interview has the conclusion already");
}
