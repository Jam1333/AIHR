using Domain.Abstractions.Errors;
using FluentResults;

namespace Domain.Errors;

public static class VacancyErrors
{
    public static Error NotFound(Guid id) => new NotFoundError($"Vacancy with id='{id}' not found");
    public static readonly Error WrongAuthor = new AccessDeniedError("You are not the author of the vacancy");
}
