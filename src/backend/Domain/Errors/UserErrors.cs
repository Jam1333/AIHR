using Domain.Abstractions.Errors;
using FluentResults;

namespace Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid id) => new NotFoundError($"User with id='{id}' not found");
    public static Error EmailAlreadyExists(string email) => new ConflictError($"User with email='{email}' already exists");
    public static readonly Error WrongEmailOrPassword = new UnauthorizedError("Wrong email or password");
    public static readonly Error WrongUser = new AccessDeniedError("Wrong user");
}
