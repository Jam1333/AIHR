using FluentResults;
using Mediator;

namespace Application.Interviews.Commands.GenerateConclusion;

public record GenerateInterviewConclusionCommand(Guid Id) : ICommand<Result<GenerateInterviewConclusionResponse>>;

public record GenerateInterviewConclusionResponse(string Conclusion);
