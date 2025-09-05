using Application.Abstractions.AI;
using Domain.ValueObjects;
using Infrastructure.Utils;
using System.Collections.Concurrent;

namespace Infrastructure.AI;

internal sealed class CandidateAnalyzer : ICandidateAnalyzer
{
    public RequirementsTotal CompareRequirements(
        IDictionary<string, Requirement[]> targetRequirements, 
        IDictionary<string, Requirement[]> candidateRequirements, 
        IDictionary<string, double> weights)
    {
        var requirementResults = new Dictionary<string, RequirementResult[]>();

        foreach (var targetCategory in targetRequirements)
        {
            if (!candidateRequirements.ContainsKey(targetCategory.Key))
            {
                continue;
            }

            var categoryRequirementResults = new List<RequirementResult>();

            foreach (Requirement targetRequirement in targetCategory.Value)
            {
                double maxSimilarity = candidateRequirements[targetCategory.Key].Max(r => 
                    SimilarityCalculator.CalculateCosineSimilarity(
                        targetRequirement.Embeddings, 
                        r.Embeddings));
                categoryRequirementResults.Add(new RequirementResult(targetRequirement.Text, maxSimilarity));
            }

            requirementResults[targetCategory.Key] = categoryRequirementResults.ToArray();
        }

        double totalSimilarity = CalculateTotalSimilarity(requirementResults, weights);

        return new RequirementsTotal(requirementResults, totalSimilarity);
    }

    public Dictionary<string, RequirementsTotal> CompareRequirementsRange(
        IDictionary<string, Requirement[]> targetRequirements, 
        IDictionary<string, IDictionary<string, Requirement[]>> candidateRequirementsRange, 
        IDictionary<string, double> weights)
    {
        var resultDictionary = new Dictionary<string, RequirementsTotal>();

        Parallel.ForEach(
            candidateRequirementsRange, 
            candidateRequirements =>
            {
                resultDictionary[candidateRequirements.Key] = CompareRequirements(targetRequirements, candidateRequirements.Value, weights);
            });

        return resultDictionary;
    }

    private static double CalculateTotalSimilarity(IDictionary<string, RequirementResult[]> requirementResults, IDictionary<string, double> weights)
    {
        double totalSimilarity = 0;

        foreach (var categoryRequirementResults in requirementResults)
        {
            if (!weights.ContainsKey(categoryRequirementResults.Key))
            {
                continue;
            }

            double categoryTotalSimilarity = categoryRequirementResults.Value.Sum(r => r.Similarity) / categoryRequirementResults.Value.Length;

            totalSimilarity += categoryTotalSimilarity * weights[categoryRequirementResults.Key];
        }

        return totalSimilarity;
    }
}
