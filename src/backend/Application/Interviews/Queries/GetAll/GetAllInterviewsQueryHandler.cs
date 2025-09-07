using Application.Interviews.Queries.GetById;
using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mediator;

namespace Application.Interviews.Queries.GetAll;

internal sealed class GetAllInterviewsQueryHandler(
    IInterviewRepository interviewRepository) : IQueryHandler<GetAllInterviewsQuery, IEnumerable<InterviewResponse>>
{
    public async ValueTask<IEnumerable<InterviewResponse>> Handle(GetAllInterviewsQuery query, CancellationToken cancellationToken)
    {
        List<Interview> interviews = await interviewRepository.GetAllAsync(query.VacancyId);

        return interviews.Select(i => i.MapToInterviewResponse());
    }
}
