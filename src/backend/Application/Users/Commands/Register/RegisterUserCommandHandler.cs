using Application.Abstractions.Cryptography;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.Commands.Register;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IHasher hasher) : ICommandHandler<RegisterUserCommand, Result<Guid>>
{
    public async ValueTask<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        bool existsWithEmail = await userRepository.ExistsWithEmailAsync(command.Email);

        if (existsWithEmail)
        {
            return UserErrors.EmailAlreadyExists(command.Email);
        }

        string passwordHash = hasher.HashPassword(command.Password);

        var user = User.Create(
            command.Username,
            command.Email,
            passwordHash);

        await userRepository.CreateAsync(user);

        return user.Id;
    }
}
