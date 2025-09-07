using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.AnswerLastQuestion;

public record AnswerInterviewLastQuestionCommand(Guid Id, string Answer) : ICommand<Result<AnswerInterviewLastQuestionResponse>>;

public record AnswerInterviewLastQuestionResponse(Guid? InterviewMessageId = null);
