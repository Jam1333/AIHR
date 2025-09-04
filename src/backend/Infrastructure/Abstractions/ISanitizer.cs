namespace Infrastructure.Abstractions;

public interface ISanitizer
{
    /// <summary>
    /// Leaves only letters, digits and spaces
    /// </summary>
    /// <param name="text">Dangerous text</param>
    /// <returns>Sanitized text</returns>
    string Sanitize(string text);
}
