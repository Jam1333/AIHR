using Application.Abstractions.Messaging;
using Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class EventBusExtensions
{
    public static IServiceCollection AddInMemoryMessageBus(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryMessageQueue>();

        services.AddSingleton<IEventBus, EventBus>();
        services.AddHostedService<EventProcessorJob>();

        return services;
    }
}
