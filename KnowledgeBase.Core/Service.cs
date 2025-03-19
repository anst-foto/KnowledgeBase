using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;

using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

public record ConnectConfig(string ConnectionString, string DatabaseName, string CollectionName);

public class Service : IService
{
    private readonly ConnectConfig _config;
    
    private string ConnectionString => _config.ConnectionString;
    private string DatabaseName => _config.DatabaseName;
    private string CollectionName => _config.CollectionName;
    
    private readonly IMongoCollection<Article> _collection;
    
    public Service(ConnectConfig config)
    {
        _config = config;

        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        _collection = db.GetCollection<Article>(CollectionName);
    }
    
    public void Create(Article article) => 
        _collection.InsertOne(article);

    public void Update(Article article) => 
        _collection.ReplaceOne(a => a.Id == article.Id, article);

    public void Delete(ObjectId id) => 
        _collection.DeleteOne(a => a.Id == id);

    public IEnumerable<Article> GetAll() => 
        _collection
            .Find(new BsonDocument())
            .ToList();

    public Article? GetBy(ObjectId id) => 
        _collection
            .Find(a => a.Id == id)
            .SingleOrDefault();

    public IEnumerable<Article>? GetBy(IEnumerable<string> tags) => 
        _collection
            .Find(a => a.Tags.Any(tags.Contains))
            .ToList();
}