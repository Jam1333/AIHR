using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.Queries.GetById;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, Result<UserResponse>>
{
    public async ValueTask<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(query.Id);

        if (user is null)
        {
            return UserErrors.NotFound(query.Id);
        }

        return user.MapToUserResponse();
    }
}
