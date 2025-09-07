using Application.Abstractions.Messaging;
using Application.Interviews.Commands.GenerateResult;
using Domain.Events;
using Domain.Extensions;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Interviews.Events;

internal sealed class InterviewEndedEventHandler(
    IServiceScopeFactory serviceScopeFactory) : IEventHandler<InterviewEndedEvent>
{
    public async ValueTask Handle(InterviewEndedEvent interviewEndedEvent, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var result = await mediator.Send(new GenerateInterviewResultCommand(interviewEndedEvent.Id, interviewEndedEvent.UserId));

        if (result.IsFailed)
        {
            throw result.ToApplicationException();
        }
    }
}
