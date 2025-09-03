using Application.Users.Queries.GetById;
using Mediator;

namespace Application.Users.Queries.GetAll;

public record GetAllUsersQuery : IQuery<IEnumerable<UserResponse>>
{
    public static readonly GetAllUsersQuery Instance = new GetAllUsersQuery();
}
