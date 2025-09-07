using Domain.Entities;

namespace Application.Abstractions.AI;

public interface IInterviewer
{
    Task<List<InterviewMessage>> InitializeInterviewAsync(string vacancyText, string language);
    Task<InterviewMessage> GenerateNextQuestionAsync(IEnumerable<InterviewMessage> interviewMessages);
}
