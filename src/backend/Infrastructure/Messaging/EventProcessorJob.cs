using Domain.Primitives;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging;

internal sealed class EventProcessorJob(
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var queue = scope.ServiceProvider.GetRequiredService<InMemoryMessageQueue>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<EventProcessorJob>>();

        await foreach (IEvent @event in queue.Reader.ReadAllAsync())
        {
            try
            {
                await publisher.Publish(@event);
            }
            catch (Exception ex)
            {
                logger.LogError("Event error occured: {Message}", ex.Message);
            }
        }
    }
}
