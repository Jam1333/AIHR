using Application.Users.Queries.GetById;
using Domain.Entities;

namespace Application.Mapper;

internal static class UserMappingExtensions
{
    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse(
            user.Id, 
            user.Username, 
            user.Email, 
            user.CreatedOnUtc);
    }
}
