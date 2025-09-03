using FluentResults;
using Mediator;

namespace Application.Users.Commands.Login;

public record LoginUserCommand(string Email, string Password) : ICommand<Result<LoginUserResponse>>;

public record LoginUserResponse(string Token);
