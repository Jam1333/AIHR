namespace Application.Abstractions.Cryptography;

public interface IHasher
{
    string HashPassword(string password);
    bool Verify(string password, string hash);
}
