using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Events;

public record AnalysisCreatedEvent(Guid Id, IEnumerable<FileContent> Files) : IEvent;
