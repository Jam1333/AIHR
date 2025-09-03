using Domain.Entities;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Generate(User user);
}
