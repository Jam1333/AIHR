using Domain.Entities;
using FluentResults;
using Mediator;

namespace Application.Analyses.Queries.GetById;

public record GetAnalysisByIdQuery(Guid Id) : IQuery<Result<AnalysisResponse>>;

public record AnalysisResponse(
    Guid Id,
    string Title,
    Dictionary<string, double> Weights,
    IEnumerable<ResumeResult> ResumeResults,
    bool IsLoaded,
    Guid VacancyId,
    Guid UserId,
    DateTime CreatedOnUtc);
