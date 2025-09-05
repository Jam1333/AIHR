using Domain.Primitives;

namespace Domain.Entities;

public sealed class Analysis : Entity
{
    public string Title { get; private set; }
    public Dictionary<string, double> Weights { get; private set; }
    public IEnumerable<ResumeResult> ResumeResults { get; private set; }
    public bool IsLoaded { get; private set; }
    public Guid VacancyId { get; private set; }
    public Guid UserId { get; private set; }

    private Analysis(
        string title,
        Dictionary<string, double> weights,
        IEnumerable<ResumeResult> resumeResults,
        bool isLoaded,
        Guid vacancyId,
        Guid userId)
    {
        Title = title;
        Weights = weights;
        ResumeResults = resumeResults;
        IsLoaded = isLoaded;
        VacancyId = vacancyId;
        UserId = userId;
    }

    public static Analysis Create(
        string title,
        Dictionary<string, double> weights, 
        Vacancy vacancy)
    {
        return new Analysis(
            title,
            weights,
            [],
            false,
            vacancy.Id,
            vacancy.UserId);
    }

    public void LoadResumeResults(IEnumerable<ResumeResult> resumeResults)
    {
        ResumeResults = resumeResults;
        IsLoaded = true;
    }
}
