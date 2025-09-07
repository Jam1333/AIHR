using Domain.Primitives;

namespace Domain.Events;

public record InterviewEndedEvent(
    Guid Id, 
    Guid UserId) : IEvent;
