using Application.Mapper;
using Application.Vacancies.Queries.GetById;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mediator;

namespace Application.Vacancies.Queries.GetAll;

internal sealed class GetAllVacanciesQueryHandler(
    IVacancyRepository vacancyRepository) : IQueryHandler<GetAllVacanciesQuery, IEnumerable<VacancyResponse>>
{
    public async ValueTask<IEnumerable<VacancyResponse>> Handle(GetAllVacanciesQuery query, CancellationToken cancellationToken)
    {
        List<Vacancy> vacancies = await vacancyRepository.GetAllAsync(query.UserId);

        return vacancies.Select(v => v.MapToVacancyResponse());
    }
}
