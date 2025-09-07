using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

internal sealed class InterviewRepository : IInterviewRepository
{
    private const string CollectionName = "interviews";

    private readonly IMongoCollection<Interview> _interviewsCollection;

    public InterviewRepository(IMongoDatabase database)
    {
        _interviewsCollection = database.GetCollection<Interview>(CollectionName);
    }

    public Task CreateAsync(Interview interview)
    {
        return _interviewsCollection.InsertOneAsync(interview);
    }

    public Task DeleteAsync(Guid id)
    {
        return _interviewsCollection.DeleteOneAsync(i => i.Id == id);
    }

    public Task DeleteByVacancyIdAsync(Guid vacancyId)
    {
        return _interviewsCollection.DeleteManyAsync(i => i.VacancyId == vacancyId);
    }

    public Task<bool> ExistsWithIdAsync(Guid id)
    {
        return _interviewsCollection.Find(i => i.Id == id).AnyAsync();
    }

    public Task<List<Interview>> GetAllAsync(Guid? vacancyId = null)
    {
        var builder = Builders<Interview>.Filter;

        var filter = builder.Empty;

        if (vacancyId is not null)
        {
            filter &= builder.Where(i => i.VacancyId == vacancyId);
        }

        var projection = Builders<Interview>.Projection.Exclude(i => i.InterviewMessages).Exclude(i => i.ResumeText);

        return _interviewsCollection.Find(filter).Project<Interview>(projection).ToListAsync();
    }

    public Task<Interview?> GetByIdAsync(Guid id)
    {
        return _interviewsCollection.Find(i => i.Id == id).SingleOrDefaultAsync() as Task<Interview?>;
    }

    public Task UpdateAsync(Interview interview)
    {
        return _interviewsCollection.ReplaceOneAsync(i => i.Id == interview.Id, interview);
    }
}
