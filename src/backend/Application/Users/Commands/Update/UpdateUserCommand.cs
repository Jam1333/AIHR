using FluentResults;
using Mediator;

namespace Application.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id, 
    string Username, 
    string Email,
    Guid CurrentUserId) : ICommand<Result<Unit>>;
