using Application.Interviews.Queries.GetById;
using Mediator;

namespace Application.Interviews.Queries.GetAll;

public record GetAllInterviewsQuery(Guid? VacancyId) : IQuery<IEnumerable<InterviewResponse>>;
