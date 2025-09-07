using Application.Abstractions.AI;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Microsoft.Extensions.AI;

namespace Infrastructure.AI;

internal sealed class RecruitmentAnalyzer(
    IChatClient chatClient,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    ISanitizer sanitizer) : IRecruitmentAnalyzer
{
    public async Task<Dictionary<string, string[]>> GetRequirementsAsync(string recruitmentText, string language, string[] categories)
    {
        string prompt = $"""
            TEXT START
            '''
            {sanitizer.Sanitize(recruitmentText)}
            '''
            END OF TEXT

            Extract and categorize the requirements from the following vacancy/resume/interview text into the specified categories: 
            [{string.Join(',', categories.Select(c => $"'{sanitizer.Sanitize(c)}'"))}].

            Return a valid JSON object where each key is a category and its value is a concise array of standardized, 
            short phrases (e.g., "Python", "Project Management", "5+ years", "English Fluent"). 
            Omit categories where no data is found.

            Language: Respond with the "{sanitizer.Sanitize(language)}" language.
            """;

        var response = await chatClient.GetResponseAsync<Dictionary<string, string[]>>(prompt);

        return response.Result;
    }

    public async Task<Dictionary<string, Requirement[]>> CreateRequirementsEmbeddingsAsync(IDictionary<string, string[]> requirements)
    {
        var requirementsEmbeddings = new Dictionary<string, Requirement[]>();

        foreach (var category in requirements)
        {
            Requirement[] requirementsWithEmbeddings = new Requirement[category.Value.Length];

            for (int i = 0; i < requirementsWithEmbeddings.Length; i++)
            {
                var embeddings = await embeddingGenerator.GenerateAsync(category.Value[i]);
                requirementsWithEmbeddings[i] = new Requirement(category.Value[i], embeddings.Vector.ToArray());
            }

            requirementsEmbeddings[category.Key] = requirementsWithEmbeddings;
        }

        return requirementsEmbeddings;
    }
}
