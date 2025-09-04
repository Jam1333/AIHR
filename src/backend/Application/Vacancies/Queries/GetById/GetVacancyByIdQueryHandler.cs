using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Vacancies.Queries.GetById;

internal sealed class GetVacancyByIdQueryHandler(
    IVacancyRepository vacancyRepository) : IQueryHandler<GetVacancyByIdQuery, Result<VacancyResponse>>
{
    public async ValueTask<Result<VacancyResponse>> Handle(GetVacancyByIdQuery query, CancellationToken cancellationToken)
    {
        Vacancy? vacancy = await vacancyRepository.GetByIdAsync(query.Id);

        if (vacancy is null)
        {
            return VacancyErrors.NotFound(query.Id);
        }

        return vacancy.MapToVacancyResponse();
    }
}
