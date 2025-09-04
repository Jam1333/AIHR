using Domain.Primitives;
using System.Threading.Channels;

namespace Infrastructure.Messaging;

internal sealed class InMemoryMessageQueue
{
    private readonly Channel<IEvent> _channel = Channel.CreateUnbounded<IEvent>();

    public ChannelWriter<IEvent> Writer => _channel.Writer;
    public ChannelReader<IEvent> Reader => _channel.Reader;
}
