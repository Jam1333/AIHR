using Domain.Abstractions.Errors;
using FluentResults;
using FluentValidation;
using Mediator;

namespace Application.Abstractions.Behavior;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
    where TResponse : class
{
    public ValueTask<TResponse> Handle(
        TRequest request, 
        MessageHandlerDelegate<TRequest, TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);


        var errors = validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => validationFailure.ErrorMessage)
            .ToList();

        if (!errors.Any())
        {
            return next(request, cancellationToken);
        }

        TResponse? validationError = CreateValidationError(errors);

        if (validationError is null)
        {
            throw new ApplicationException("Unexpected validation error occured");
        }

        return ValueTask.FromResult(validationError);
    }

    private static TResponse? CreateValidationError(List<string> errors)
    {
        var resultType = typeof(Result<>);

        var responseGenericType = typeof(TResponse).GetGenericArguments().FirstOrDefault();

        if (responseGenericType is null)
        {
            return null;
        }

        resultType = resultType.MakeGenericType(responseGenericType);

        var result = Activator.CreateInstance(resultType);

        if (result is null)
        {
            return null;
        }

        var methodInfo = resultType.GetMethod("WithError", [typeof(IError)]);

        if (methodInfo is null)
        {
            return null;
        }

        var validationError = new ValidationError(errors);

        result = methodInfo.Invoke(result, [validationError]);

        return result as TResponse;
    }
}
