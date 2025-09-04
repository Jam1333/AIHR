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
        bool exists = await userRepository.ExistsWithIdAsync(command.Id);

        if (!exists)
        {
            return UserErrors.NotFound(command.Id);
        }

        await userRepository.DeleteAsync(command.Id);

        return Unit.Value;
    }
}
