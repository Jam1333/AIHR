using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.GenerateResult;

public record GenerateInterviewResultCommand(Guid Id, Guid CurrentUserId) : ICommand<Result<Unit>>;
