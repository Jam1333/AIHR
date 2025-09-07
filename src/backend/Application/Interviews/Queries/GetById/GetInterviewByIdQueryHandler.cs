using Application.Mapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Mediator;

namespace Application.Interviews.Queries.GetById;

internal sealed class GetInterviewByIdQueryHandler(
    IInterviewRepository interviewRepository) : IQueryHandler<GetInterviewByIdQuery, Result<InterviewResponse>>
{
    public async ValueTask<Result<InterviewResponse>> Handle(GetInterviewByIdQuery query, CancellationToken cancellationToken)
    {
        Interview? interview = await interviewRepository.GetByIdAsync(query.Id);

        if (interview is null)
        {
            return InterviewErrors.NotFound(query.Id);
        }

        return interview.MapToInterviewResponse();
    }
}
