using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Analyses.Queries.GetById;

internal sealed class GetAnalysisByIdQueryHandler(
    IAnalysisRepository analysisRepository) : IQueryHandler<GetAnalysisByIdQuery, Result<AnalysisResponse>>
{
    public async ValueTask<Result<AnalysisResponse>> Handle(GetAnalysisByIdQuery query, CancellationToken cancellationToken)
    {
        Analysis? analysis = await analysisRepository.GetByIdAsync(query.Id);

        if (analysis is null)
        {
            return AnalysisErrors.NotFound(query.Id);
        }

        return analysis.MapToAnalysisResponse();
    }
}
