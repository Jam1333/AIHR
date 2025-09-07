using Domain.Primitives;

namespace Domain.Entities;

public sealed class InterviewMessage : Entity
{
    public string Question { get; private set; }
    public string? Answer { get; private set; }
    public DateTime? AnsweredOnUtc { get; private set; }

    public bool HasAnswer => Answer is not null;

    private InterviewMessage(string question, string? answer)
    {
        Question = question;
        Answer = answer;
    }

    public static InterviewMessage Create(string question)
    {
        return new InterviewMessage(question, null);
    }

    public static InterviewMessage Create(string question, string answer)
    {
        return new InterviewMessage(question, answer);
    }

    public void AnswerQuestion(string answer)
    {
        Answer = answer;
        AnsweredOnUtc = DateTime.UtcNow;
    }
}
