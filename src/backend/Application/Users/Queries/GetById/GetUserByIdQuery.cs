using FluentResults;
using Mediator;

namespace Application.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<Result<UserResponse>>;

public record UserResponse(
    Guid Id, 
    string Username,
    string Email, 
    DateTime CreatedOnUtc);
