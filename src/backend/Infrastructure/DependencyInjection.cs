using Application.Abstractions.Authentication;
using Application.Abstractions.Cryptography;
using Domain.Abstractions.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Cryptography;
using Infrastructure.Repositories;
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
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        services.AddSingleton(new MongoClient(configuration.GetConnectionString("Database")).GetDatabase("ai_hr"));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<IHasher, Hasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        string ollamaConnectionString = configuration.GetConnectionString("Ollama") ?? throw new ApplicationException("Ollama connection string not set");

        services.AddEmbeddingGenerator(
            new OllamaApiClient(
                ollamaConnectionString,
                configuration["Models:EmbeddingGenerator"] ?? throw new ApplicationException("Embedding generator model not set")));

        services.AddChatClient(
            new OllamaApiClient(
                ollamaConnectionString,
                configuration["Models:ChatClient"] ?? throw new ApplicationException("Chat client not set")));

        return services;
    }
}
