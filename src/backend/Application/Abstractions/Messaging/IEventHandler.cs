using Domain.Primitives;
using Mediator;

namespace Application.Abstractions.Messaging;

internal interface IEventHandler<T> : INotificationHandler<T>
    where T : IEvent;
