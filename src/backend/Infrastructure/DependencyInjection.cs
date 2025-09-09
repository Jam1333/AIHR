using Application.Abstractions.AI;
using Application.Abstractions.Authentication;
using Application.Abstractions.Cryptography;
using Application.Abstractions.FileSystem;
using Domain.Abstractions.Repositories;
using Infrastructure.Abstractions;
using Infrastructure.AI;
using Infrastructure.Authentication;
using Infrastructure.Cryptography;
using Infrastructure.Extensions;
using Infrastructure.FileSystem;
using Infrastructure.Repositories;
using Infrastructure.Sanitization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OllamaSharp;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFileReader, FileReader>();

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        services.AddSingleton(new MongoClient(configuration.GetConnectionString("Database")).GetDatabase("ai_hr"));

        services.AddInMemoryMessageBus();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IVacancyRepository, VacancyRepository>();

        services.AddScoped<IAnalysisRepository, AnalysisRepository>();
        services.AddScoped<IInterviewRepository, InterviewRepository>();

        services.AddSingleton<IHasher, Hasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        string ollamaConnectionString = configuration.GetConnectionString("Ollama") ?? throw new ApplicationException("Ollama connection string not set");

        services.AddEmbeddingGenerator(
            new OllamaApiClient(
                ollamaConnectionString,
                configuration["Models:EmbeddingGenerator"] ?? throw new ApplicationException("Embedding generator model not set")));

        services.AddChatClient(
            new OllamaApiClient(
                new HttpClient 
                {
                    BaseAddress = new Uri(ollamaConnectionString),
                    Timeout = TimeSpan.FromMinutes(10),
                },
                configuration["Models:ChatClient"] ?? throw new ApplicationException("Chat client not set")));

        services.AddSingleton<ISanitizer, Sanitizer>();

        services.AddSingleton<IRecruitmentAnalyzer, RecruitmentAnalyzer>();
        services.AddSingleton<ICandidateAnalyzer, CandidateAnalyzer>();

        services.AddSingleton<IInterviewer, Interviewer>();
        services.AddSingleton<IInterviewSummarizer, InterviewSummarizer>();

        return services;
    }
}
