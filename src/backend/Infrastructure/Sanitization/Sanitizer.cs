using Infrastructure.Abstractions;
using System.Text.RegularExpressions;

namespace Infrastructure.Sanitization;

internal sealed class Sanitizer : ISanitizer
{
    public string Sanitize(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return Regex.Replace(text, @"[^\w\s\n\r,;:.]", string.Empty, RegexOptions.Compiled);
    }
}
