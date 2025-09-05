using Domain.Primitives;
using Mediator;

namespace Domain.Events;

public record VacancyCreatedEvent(Guid VacancyId, string[] Categories) : IEvent;
