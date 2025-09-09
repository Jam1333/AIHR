using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Errors;
using Domain.ValueObjects;
using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.ProvideContactInformation;

internal sealed class ProvideInterviewContactInformationCommandHandler(
    IInterviewRepository interviewRepository) : ICommandHandler<ProvideInterviewContactInformationCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(ProvideInterviewContactInformationCommand command, CancellationToken cancellationToken)
    {
        Interview? interview = await interviewRepository.GetByIdAsync(command.Id);

        if (interview is null)
        {
            return InterviewErrors.NotFound(command.Id);
        }

        var contactInformation = new ContactInformation(
            command.Email, 
            command.PhoneNumber);

        interview.UpdateContactInformation(contactInformation);

        await interviewRepository.UpdateAsync(interview);

        return Unit.Value;
    }
}
