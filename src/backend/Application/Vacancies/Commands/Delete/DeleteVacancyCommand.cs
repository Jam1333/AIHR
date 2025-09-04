using FluentResults;
using Mediator;

namespace Application.Vacancies.Commands.Delete;

public record DeleteVacancyCommand(Guid Id, Guid CurrentUserId) : ICommand<Result<Unit>>;
