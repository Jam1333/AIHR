using Application.Interviews.Queries.GetById;
using Domain.Entities;

namespace Application.Mapper;

internal static class InterviewMappingExtensions
{
    public static InterviewResponse MapToInterviewResponse(this Interview interview)
    {
        return new InterviewResponse(
            interview.Id,
            interview.Title,
            interview.ResumeText ?? "",
            interview.Language,
            interview.Weights,
            interview.MaxMessagesCount,
            interview.InterviewMessages ?? [],
            interview.ContactInformation,
            interview.InterviewResult,
            interview.Conclusion,
            interview.HasEnded,
            interview.VacancyId,
            interview.UserId,
            interview.CreatedOnUtc);
    }
}
