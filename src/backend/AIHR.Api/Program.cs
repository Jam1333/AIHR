using AIHR.Api.Middlewares;
using Application;
using Carter;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowClient", builder =>
{
    builder
        .WithOrigins("http://localhost")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
}));

builder.Services.AddOpenApi();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCarter();

var app = builder.Build();

app.UseCors("AllowClient");

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/ping", () => "pong");
app.MapGet("/data", [Authorize] () => "data");

app.MapGet("/chat-client", async ([FromQuery] string prompt, IChatClient chatClient) =>
{
    var response = await chatClient.GetResponseAsync(prompt);

    return response.Text;
});

app.MapGet("/text-embeddings", async ([FromQuery] string text, IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator) =>
{
    var response = await embeddingGenerator.GenerateAsync(text);

    return response.Vector;
});

app.MapCarter();

app.Run();
