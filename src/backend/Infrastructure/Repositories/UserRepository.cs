using Domain.Abstractions.Repositories;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private const string CollectionName = "users";

    private readonly IMongoCollection<User> _usersCollection;

    public UserRepository(IMongoDatabase database)
    {
        _usersCollection = database.GetCollection<User>(CollectionName);
    }

    public Task CreateAsync(User user)
    {
        return _usersCollection.InsertOneAsync(user);
    }

    public Task DeleteAsync(Guid id)
    {
        return _usersCollection.FindOneAndDeleteAsync(u => u.Id == id);
    }

    public Task<bool> ExistsWithEmailAsync(string email)
    {
        return _usersCollection.Find(u => u.Email == email).AnyAsync();
    }

    public Task<bool> ExistsWithIdAsync(Guid id)
    {
        return _usersCollection.Find(u => u.Id == id).AnyAsync();
    }

    public Task<List<User>> GetAllAsync()
    {
        return _usersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _usersCollection.Find(u => u.Email == email).SingleOrDefaultAsync() as Task<User?>;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _usersCollection.Find(u => u.Id == id).SingleOrDefaultAsync() as Task<User?>;
    }

    public Task UpdateAsync(User user)
    {
        return _usersCollection.FindOneAndReplaceAsync(u => u.Id == user.Id, user);
    }
}
