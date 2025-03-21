// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;

using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

public record ConnectConfig(string ConnectionString, string DatabaseName, string CollectionName);

public class Service : IService, IDisposable
{
    private readonly ConnectConfig _config;
    
    private string ConnectionString => _config.ConnectionString;
    private string DatabaseName => _config.DatabaseName;
    private string CollectionName => _config.CollectionName;
    
    private readonly IMongoClient _client;
    
    public Service(ConnectConfig config)
    {
        _config = config;

        _client = new MongoClient(ConnectionString);
    }

    public void Create(Article article) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .InsertOne(article);
        

    public void Update(Article article) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .ReplaceOne(a => a.Id == article.Id, article);

    public void Delete(ObjectId id) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .DeleteOne(a => a.Id == id);

    public IEnumerable<Article> GetAll() => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(new BsonDocument())
            .ToList();

    public Article? GetBy(ObjectId id) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(a => a.Id == id)
            .SingleOrDefault();

    public IEnumerable<Article>? GetBy(IEnumerable<string> tags) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(a => a.Tags.Any(tags.Contains))
            .ToList();

    public void Dispose()
    {
        _client.Dispose();
    }
}