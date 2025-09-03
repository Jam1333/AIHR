using Application.Mapper;
using Application.Users.Queries.GetById;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mediator;

namespace Application.Users.Queries.GetAll;

internal sealed class GetAllUsersQueryHandler(
    IUserRepository userRepository) : IQueryHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    public async ValueTask<IEnumerable<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        List<User> users = await userRepository.GetAllAsync();

        return users.Select(u => u.MapToUserResponse());
    }
}
