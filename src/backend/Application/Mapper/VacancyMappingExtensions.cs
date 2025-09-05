using Application.Vacancies.Queries.GetById;
using Domain.Entities;

namespace Application.Mapper;

internal static class VacancyMappingExtensions
{
    public static VacancyResponse MapToVacancyResponse(this Vacancy vacancy)
    {
        return new VacancyResponse(
            vacancy.Id,
            vacancy.Title,
            vacancy.Language, 
            vacancy.Text ?? "",
            vacancy.Requirements?.Select(p => (p.Key, p.Value.Select(r => r.Text).ToArray())).ToDictionary(r => r.Key, r => r.Item2) ?? [],
            vacancy.IsLoaded,
            vacancy.UserId,
            vacancy.CreatedOnUtc);
    }
}
