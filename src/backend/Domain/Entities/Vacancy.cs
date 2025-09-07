using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Vacancy : Entity
{
    public string Title { get; private set; }
    public string Language { get; private set; }
    public string Text { get; set; }
    public Dictionary<string, Requirement[]> Requirements { get; private set; }
    public bool IsLoaded { get; private set; }
    public Guid UserId { get; private set; }

    public string[] Categories => Requirements.Keys.ToArray();

    public Vacancy(
        string title, 
        string language, 
        string text, 
        Dictionary<string, Requirement[]> requirements, 
        bool isLoaded, 
        Guid userId)
    {
        Title = title;
        Language = language;
        Text = text;
        Requirements = requirements;
        IsLoaded = isLoaded;
        UserId = userId;
    }

    public static Vacancy Create(
        string title, 
        string language, 
        string text,
        User user)
    {
        return new Vacancy(
            title, 
            language, 
            text, 
            [], 
            false, 
            user.Id);
    }

    public void LoadRequirements(Dictionary<string, Requirement[]> requirements)
    {
        Requirements = requirements;
        IsLoaded = true;
    }
}
