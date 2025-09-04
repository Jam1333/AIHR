﻿using Domain.Primitives;

namespace Application.Abstractions.Messaging;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) 
        where T : class, IEvent;
}
