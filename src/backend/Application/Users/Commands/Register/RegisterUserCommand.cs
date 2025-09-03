using FluentResults;
using Mediator;

namespace Application.Users.Commands.Register;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<Result<Guid>>;
