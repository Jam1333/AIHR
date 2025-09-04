using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class Vacancy : Entity
{
    public string Title { get; private set; }
    public string Language { get; private set; }
    public Dictionary<string, Requirement[]> Requirements { get; private set; }
    public bool IsLoaded { get; private set; }
    public Guid UserId { get; private set; }

    public Vacancy(
        string title, 
        string language, 
        Dictionary<string, Requirement[]> requirements, 
        bool isLoaded, 
        Guid userId)
    {
        Title = title;
        Language = language;
        Requirements = requirements;
        IsLoaded = isLoaded;
        UserId = userId;
    }

    public static Vacancy Create(
        string title, 
        string language, 
        User user)
    {
        return new Vacancy(
            title, 
            language, 
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
