using FluentResults;
using Mediator;

namespace Application.Vacancies.Queries.GetById;

public record GetVacancyByIdQuery(Guid Id) : IQuery<Result<VacancyResponse>>;

public record VacancyResponse(
    Guid Id, 
    string Title, 
    string Language, 
    string Text, 
    Dictionary<string, string[]> Requirements, 
    bool IsLoaded, 
    Guid UserId,
    DateTime CreatedOnUtc);
