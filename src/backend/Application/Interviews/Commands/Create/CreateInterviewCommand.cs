using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.Create;

public record CreateInterviewCommand(
    string Title, 
    Dictionary<string, double> Weights, 
    FileRequest ResumeFile, 
    int MaxMessagesCount, 
    Guid VacancyId, 
    Guid CurrentUserId) : ICommand<Result<Guid>>;
