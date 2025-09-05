namespace Domain.ValueObjects;

public record RequirementsTotal(Dictionary<string, RequirementResult[]> RequirementResults, double TotalSimilarity);
