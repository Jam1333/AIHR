using Domain.Entities;
using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Interviews.Queries.GetById;

public record GetInterviewByIdQuery(Guid Id) : IQuery<Result<InterviewResponse>>;

public record InterviewResponse(
    Guid Id,
    string Title,
    string ResumeText,
    string Language,
    Dictionary<string, double> Weights,
    int MaxMessagesCount,
    List<InterviewMessage> InterviewMessages,
    ContactInformation? ContactInformation,
    InterviewResult? InterviewResult,
    string? Conclusion,
    bool HasEnded,
    Guid VacancyId,
    Guid UserId,
    DateTime CreatedOnUtc);
