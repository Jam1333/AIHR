using Domain.ValueObjects;

namespace Application.Abstractions.AI;

public interface ICandidateAnalyzer
{
    RequirementsTotal CompareRequirements(
        IDictionary<string, Requirement[]> targetRequirements, 
        IDictionary<string, Requirement[]> candidateRequirements,
        IDictionary<string, double> weights);
    Dictionary<string, RequirementsTotal> CompareRequirementsRange(
        IDictionary<string, Requirement[]> targetRequirements, 
        IDictionary<string, IDictionary<string, Requirement[]>> candidateRequirementsRange,
        IDictionary<string, double> weights);
}
