using Application.Abstractions.Authentication;
using Application.Abstractions.Cryptography;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.Commands.Login;

internal sealed class LoginUserCommandHandler(
    IUserRepository userRepository,
    IHasher hasher,
    ITokenProvider jwtProvider) : ICommandHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    public async ValueTask<Result<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            return UserErrors.WrongEmailOrPassword;
        }

        bool isValidPassword = hasher.Verify(command.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            return UserErrors.WrongEmailOrPassword;
        }

        string token = jwtProvider.Generate(user);

        return new LoginUserResponse(token);
    }
}
