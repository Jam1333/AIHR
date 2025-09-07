using AIHR.Api.Extensions;
using Application.Interviews.Commands.AnswerLastQuestion;
using Carter;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.Api.Endpoints.Interviews;

public sealed class AnswerInterviewLastQuestionEndpoint : ICarterModule
{
    private record AnswerInterviewLastQuestionRequest(string Answer);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/interviews/{id:guid}/answer-question", 
            async (
                [FromRoute] Guid id, 
                [FromBody] AnswerInterviewLastQuestionRequest request, 
                IMediator mediator) =>
            {
                var command = new AnswerInterviewLastQuestionCommand(
                    id, 
                    request.Answer);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
            })
            .AllowAnonymous();
    }
}
