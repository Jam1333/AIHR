using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class InterviewResult : Entity
{
    public RequirementsTotal RequirementsTotal { get; private set; }
    public string[] RedFlags { get; private set; }
    public string[] Inconsistencies { get; private set; }

    private InterviewResult(
        RequirementsTotal requirementsTotal, 
        string[] redFlags, 
        string[] inconsistencies)
    {
        RequirementsTotal = requirementsTotal;
        RedFlags = redFlags;
        Inconsistencies = inconsistencies;
    }
    
    public static InterviewResult Create(
        RequirementsTotal requirementsTotal,
        string[] redFlags,
        string[] inconsistencies)
    {
        return new InterviewResult(
            requirementsTotal,
            redFlags,
            inconsistencies);
    }
}
