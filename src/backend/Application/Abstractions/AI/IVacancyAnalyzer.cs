using Domain.ValueObjects;

namespace Application.Abstractions.AI;

public interface IVacancyAnalyzer
{
    Task<Dictionary<string, string[]>> GetRequirementsAsync(string vacancyText, string language, string[] categories);
    Task<Dictionary<string, Requirement[]>> CreateRequirementsEmbeddingsAsync(Dictionary<string, string[]> requirements);
}
