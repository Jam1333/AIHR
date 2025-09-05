using Application.Analyses.Queries.GetById;
using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mediator;

namespace Application.Analyses.Queries.GetAll;

internal sealed class GetAllAnalysesQueryHandler(
    IAnalysisRepository analysisRepository) : IQueryHandler<GetAllAnalysesQuery, IEnumerable<AnalysisResponse>>
{
    public async ValueTask<IEnumerable<AnalysisResponse>> Handle(GetAllAnalysesQuery query, CancellationToken cancellationToken)
    {
        List<Analysis> analyses = await analysisRepository.GetAllAsync(query.VacancyId);

        return analyses.Select(a => a.MapToAnalysisResponse());
    }
}
