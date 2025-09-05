using Application.Analyses.Queries.GetById;
using Mediator;

namespace Application.Analyses.Queries.GetAll;

public record GetAllAnalysesQuery(Guid? VacancyId) : IQuery<IEnumerable<AnalysisResponse>>;
