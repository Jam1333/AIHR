using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Analyses.Commands.Create;

public record CreateAnalysisCommand(
    string Title, 
    Dictionary<string, double> Weights, 
    IEnumerable<FileRequest> Files,
    Guid VacancyId,
    Guid CurrentUserId) : ICommand<Result<Guid>>;
