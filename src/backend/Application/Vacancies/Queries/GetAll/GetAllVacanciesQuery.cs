using Application.Vacancies.Queries.GetById;
using Mediator;

namespace Application.Vacancies.Queries.GetAll;

public record GetAllVacanciesQuery(Guid? UserId) : IQuery<IEnumerable<VacancyResponse>>;
