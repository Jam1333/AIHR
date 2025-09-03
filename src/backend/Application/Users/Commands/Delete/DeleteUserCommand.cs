using FluentResults;
using Mediator;

namespace Application.Users.Commands.Delete;

public record DeleteUserCommand(
    Guid Id, 
    Guid CurrentUserId) : ICommand<Result<Unit>>;
