using Domain.ValueObjects;

namespace Application.Abstractions.AI;

public interface IRecruitmentAnalyzer
{
    Task<Dictionary<string, string[]>> GetRequirementsAsync(string text, string language, string[] categories);
    Task<Dictionary<string, Requirement[]>> CreateRequirementsEmbeddingsAsync(IDictionary<string, string[]> requirements);
}
