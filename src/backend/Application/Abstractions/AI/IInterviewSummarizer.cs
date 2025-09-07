using Domain.Entities;

namespace Application.Abstractions.AI;

public interface IInterviewSummarizer
{
    Task<string[]> GetCandidateRedFlags(IEnumerable<InterviewMessage> interviewMessages, string language);
    Task<string[]> GetCandidateInconsistencies(IEnumerable<InterviewMessage> interviewMessages, string resumeText, string language);
    Task<string> GetInterviewConclusion(IEnumerable<InterviewMessage> interviewMessages, string language);
}
