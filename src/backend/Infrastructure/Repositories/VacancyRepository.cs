using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class VacancyRepository : IVacancyRepository
{
    private const string CollectionName = "vacancies";

    private readonly IMongoCollection<Vacancy> _vacanciesCollection;

    public VacancyRepository(IMongoDatabase database)
    {
        _vacanciesCollection = database.GetCollection<Vacancy>(CollectionName);
    }

    public Task CreateAsync(Vacancy vacancy)
    {
        return _vacanciesCollection.InsertOneAsync(vacancy);
    }

    public Task DeleteAsync(Guid id)
    {
        return _vacanciesCollection.FindOneAndDeleteAsync(v => v.Id == id);
    }

    public Task<bool> ExistsWithIdAsync(Guid id)
    {
        return _vacanciesCollection.Find(v => v.Id == id).AnyAsync();
    }

    public Task<List<Vacancy>> GetAllAsync(Guid? userId = null)
    {
        var builder = Builders<Vacancy>.Filter;

        var filter = builder.Empty;

        if (userId is not null)
        {
            filter &= builder.Where(v => v.UserId == userId);
        }

        var projection = Builders<Vacancy>.Projection
            .Exclude(v => v.Requirements)
            .Exclude(v => v.Text);

        return _vacanciesCollection
            .Find(filter)
            .Project<Vacancy>(projection)
            .ToListAsync();
    }

    public Task<Vacancy?> GetByIdAsync(Guid id)
    {
        return _vacanciesCollection.Find(v => v.Id == id).SingleOrDefaultAsync() as Task<Vacancy?>;
    }

    public Task UpdateAsync(Vacancy vacancy)
    {
        return _vacanciesCollection.FindOneAndReplaceAsync(v => v.Id == vacancy.Id, vacancy);
    }
}
