using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class ResumeResult : Entity
{
    public string Title { get; private set; }
    public RequirementsTotal RequirementsTotal { get; private set; }

    private ResumeResult(
        string title,
        RequirementsTotal requirementsTotal)
    {
        Title = title;
        RequirementsTotal = requirementsTotal;
    }

    public static ResumeResult Create(
        string title,
        RequirementsTotal requirementsTotal)
    {
        return new ResumeResult(
            title,
            requirementsTotal);
    }
}
