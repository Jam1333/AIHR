using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.Commands.Delete;

internal sealed class DeleteUserCommandHandler(
    IUserRepository userRepository) : ICommandHandler<DeleteUserCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
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

        await userRepository.DeleteAsync(user);

        return Unit.Value;
    }
}
