using Domain.Primitives;

namespace Domain.Entities;

public sealed class User : Entity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    private User(
        string username, 
        string email, 
        string passwordHash)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static User Create(
        string username, 
        string email, 
        string passwordHash)
    {
        return new User(
            username, 
            email, 
            passwordHash);
    }

    public void UpdateInformation(
        string username, 
        string email)
    {
        Username = username;
        Email = email;
    }
}
