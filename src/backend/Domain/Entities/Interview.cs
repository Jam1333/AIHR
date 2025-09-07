using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Interview : Entity
{
    public string Title { get; private set; }
    public string ResumeText { get; private set; }
    public string Language { get; private set; }
    public Dictionary<string, double> Weights { get; private set; }
    public int MaxMessagesCount { get; private set; }
    public List<InterviewMessage> InterviewMessages { get; private set; }
    public ContactInformation? ContactInformation { get; private set; }
    public InterviewResult? InterviewResult { get; private set; }
    public string? Conclusion { get; private set; }
    public bool HasEnded { get; private set; }
    public Guid VacancyId { get; private set; }
    public Guid UserId { get; private set; }

    public string CandidateAnswersText => string.Join('\n', InterviewMessages.Select(m => m.Answer));

    public bool HasResult => InterviewResult is not null;
    public bool HasConclusion => Conclusion is not null;

    private Interview(
        string title,
        string resumeText,
        string language,
        Dictionary<string, double> weights, 
        int maxMessagesCount, 
        List<InterviewMessage> interviewMessages, 
        ContactInformation? contact, 
        InterviewResult? interviewResult, 
        string? conclusion, 
        bool hasEnded, 
        Guid vacancyId, 
        Guid userId)
    {
        Title = title;
        ResumeText = resumeText;
        Language = language;
        Weights = weights;
        MaxMessagesCount = maxMessagesCount;
        InterviewMessages = interviewMessages;
        ContactInformation = contact;
        InterviewResult = interviewResult;
        Conclusion = conclusion;
        HasEnded = hasEnded;
        VacancyId = vacancyId;
        UserId = userId;
    }

    public static Interview Create(
        string title, 
        Dictionary<string, double> weights, 
        string resumeText, 
        int maxMessagesCount, 
        List<InterviewMessage> initialInterviewMessages,
        Vacancy vacancy)
    {
        return new Interview(
            title,
            resumeText,
            vacancy.Language,
            weights, 
            maxMessagesCount, 
            initialInterviewMessages, 
            null, 
            null, 
            null,
            false,
            vacancy.Id, 
            vacancy.UserId);
    }

    public void AddInterviewMessage(InterviewMessage interviewMessage)
    {
        InterviewMessages.Add(interviewMessage);
    }

    public void AnswerLastQuestion(string answer)
    {
        InterviewMessages.Last().AnswerQuestion(answer);

        if (InterviewMessages.Count >= MaxMessagesCount + 1)
        {
            HasEnded = true;
        }
    }

    public void UpdateContactInformation(ContactInformation contactInformation)
    {
        ContactInformation = contactInformation;
    }

    public void ProvideInterviewResult(InterviewResult interviewResult)
    {
        InterviewResult = interviewResult;
    }

    public void ProvideConclusion(string conclusion)
    {
        Conclusion = conclusion;
    }
}
