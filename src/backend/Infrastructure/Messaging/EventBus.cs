using Application.Abstractions.Messaging;
using Domain.Primitives;

namespace Infrastructure.Messaging;

internal sealed class EventBus(
    InMemoryMessageQueue queue) : IEventBus
{
    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        where T : class, IEvent
    {
        await queue.Writer.WriteAsync(@event, cancellationToken);
    }
}
