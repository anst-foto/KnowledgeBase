using KnowledgeBase.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace KnowledgeBase.Core.Test;

public class ServiceTest
{
    private readonly IService _service;

    public ServiceTest()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var databaseName = config.GetConnectionString("DatabaseName");
        var collectionName = config.GetConnectionString("CollectionName");
        
        _service = new Service(new ConnectConfig(connectionString, databaseName, collectionName));
    }
    
    [Fact]
    public void GetAllTest()
    {
        var article = new Article()
        {
            Id = new ObjectId("67d854d78fd13a37e0cff708"),
            Title = "MongoDB",
            Content = "MongoDB is a document database",
            DateOfCreation = new(year: 2025, month: 3, day: 17),
            DateOfLastUpdate = new(year: 2025, month: 3, day: 17)
        };
        article.Tags.Add("mongodb");
        article.Tags.Add("database");
        article.Tags.Add("NoSQL");
        var expectedArticles = new List<Article> { article };
        
        var actualArticles = _service.GetAll().ToList();
        
        Assert.Multiple(
            () => Assert.NotEmpty(actualArticles),
            () => Assert.Equal(expectedArticles, actualArticles));
    }
}