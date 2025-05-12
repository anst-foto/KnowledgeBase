// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using KnowledgeBase.Model;


namespace KnowledgeBase.Core.Test;

//FIXME Переделать тесты
public class ServiceTest
{
    private readonly IService _service;

    private readonly Article _article;

    public ServiceTest()
    {
        //TODO Выделить в отдельный файл
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        var databaseName = config.GetConnectionString("DatabaseName");
        var collectionName = config.GetConnectionString("CollectionName");

        _service = new Service(new ConnectConfig(connectionString, databaseName, collectionName));

        _article = new Article
        {
            Title = "MongoDB",
            Content = "MongoDB is a document database",
            DateOfCreation = new DateTime(2025, 3, 17, 12, 30, 30, DateTimeKind.Utc),
            DateOfLastUpdate = new DateTime(2025, 3, 17, 12, 30, 30, DateTimeKind.Utc)
        };
        _article.Tags.Add("mongodb");
        _article.Tags.Add("database");
        _article.Tags.Add("NoSQL");
    }

    [Fact]
    public void GetAllTest()
    {
        var expectedArticles = new List<Article> { _article };

        var actualArticles = _service.GetAllAsync();

        Assert.Multiple(
            () => Assert.NotNull(actualArticles),
            () => Assert.NotEmpty(actualArticles),
            () => Assert.Equal(expectedArticles, actualArticles));
    }

    [Fact]
    public async Task GetByIdTest()
    {
        var actualArticle =
            await _service.GetByAsync(new Guid("67d854d78fd13a37e0cff708")); //FIXME Уйти от магических чисел
        Assert.Multiple(
            () => Assert.NotNull(actualArticle),
            () => Assert.Equal(_article, actualArticle));
    }

    [Fact]
    public async Task CreateTest()
    {
        await _service.CreateAsync(_article);
        var actualArticles = _service.GetAllAsync();
        Assert.Equal(2, actualArticles.ToBlockingEnumerable().Count()); //FIXME Уйти от магических чисел

        //_service.DeleteAsync(actualArticles);
    }
}