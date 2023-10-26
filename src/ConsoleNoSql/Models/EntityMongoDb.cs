using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleNoSql.Models;

public abstract class EntityMongoDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}