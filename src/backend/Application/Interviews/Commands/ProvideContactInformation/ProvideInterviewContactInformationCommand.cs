using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.ProvideContactInformation;

public record ProvideInterviewContactInformationCommand(
    Guid Id,
    string Email, 
    string PhoneNumber) : ICommand<Result<Unit>>;
