using Infrastructure.Abstractions;

namespace Infrastructure.Sanitization;

internal sealed class Sanitizer : ISanitizer
{
    public string Sanitize(string text)
    {
        return new string(
            text
                .Trim()
                .Where(c => char.IsWhiteSpace(c) || char.IsLetter(c) || char.IsDigit(c))
                .ToArray()
        );
    }
}
