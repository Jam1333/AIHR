using Application.Models;
using FluentResults;
using Mediator;

namespace Application.Vacancies.Commands.Create;

public record CreateVacancyCommand(
    string Title,
    string Language,
    string[] Categories, 
    FileRequest File, 
    Guid UserId) : ICommand<Result<Guid>>;
