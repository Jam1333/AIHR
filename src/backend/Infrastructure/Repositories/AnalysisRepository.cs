using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

internal sealed class AnalysisRepository : IAnalysisRepository
{
    private const string CollectionName = "analyses";

    private readonly IMongoCollection<Analysis> _analysesCollection;

    public AnalysisRepository(IMongoDatabase database)
    {
        _analysesCollection = database.GetCollection<Analysis>(CollectionName);
    }

    public Task CreateAsync(Analysis analysis)
    {
        return _analysesCollection.InsertOneAsync(analysis);
    }

    public Task DeleteAsync(Guid id)
    {
        return _analysesCollection.DeleteOneAsync(a => a.Id == id);
    }

    public Task DeleteByVacancyId(Guid vacancyId)
    {
        return _analysesCollection.DeleteManyAsync(v => v.VacancyId == vacancyId);
    }

    public Task<bool> ExistsWithIdAsync(Guid id)
    {
        return _analysesCollection.Find(a => a.Id == id).AnyAsync();
    }

    public Task<List<Analysis>> GetAllAsync(Guid? vacancyId = null)
    {
        var builder = Builders<Analysis>.Filter;

        var filter = builder.Empty;

        if (vacancyId is not null)
        {
            filter &= builder.Where(a => a.VacancyId == vacancyId);
        }

        var projection = Builders<Analysis>.Projection.Exclude(v => v.ResumeResults);

        return _analysesCollection.Find(filter).Project<Analysis>(projection).ToListAsync();
    }

    public Task<Analysis?> GetByIdAsync(Guid id)
    {
        return _analysesCollection.Find(a => a.Id == id).SingleOrDefaultAsync() as Task<Analysis?>;
    }

    public Task UpdateAsync(Analysis analysis)
    {
        return _analysesCollection.ReplaceOneAsync(a => a.Id == analysis.Id, analysis);
    }
}
