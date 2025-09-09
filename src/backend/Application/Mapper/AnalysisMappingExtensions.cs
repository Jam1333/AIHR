using Application.Analyses.Queries.GetById;
using Domain.Entities;

namespace Application.Mapper;

internal static class AnalysisMappingExtensions
{
    public static AnalysisResponse MapToAnalysisResponse(this Analysis analysis)
    {
        return new AnalysisResponse(
            analysis.Id,
            analysis.Title,
            analysis.Weights,
            analysis.ResumeResults ?? [],
            analysis.IsLoaded,
            analysis.VacancyId,
            analysis.UserId,
            analysis.CreatedOnUtc);
    }
}
