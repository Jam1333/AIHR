using FluentResults;

namespace Domain.Extensions;

public static class ResultExtensions
{
    public static IError GetError(this ResultBase result)
    {
        if (result.IsSuccess || result.Errors.Count == 0)
        {
            throw new ApplicationException("There is no errors in result");
        }

        return result.Errors.First();
    }

    public static ApplicationException ToApplicationException(this ResultBase result)
    {
        IError error = result.GetError();

        return new ApplicationException(error.Message);
    }
}
