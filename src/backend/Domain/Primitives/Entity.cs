using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Primitives;

public class Entity
{
    [BsonId]
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;
}
