using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.Commands.Update;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository) : ICommandHandler<UpdateUserCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Id != command.CurrentUserId)
        {
            return UserErrors.WrongUser;
        }

        User? user = await userRepository.GetByIdAsync(command.Id);

        if (user is null)
        {
            return UserErrors.NotFound(command.Id);
        }

        user.UpdateInformation(command.Username, command.Email);

        await userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
