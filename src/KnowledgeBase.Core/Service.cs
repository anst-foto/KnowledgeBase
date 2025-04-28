// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;

using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

/// <summary>
/// Конфигурация подключения к базе данных
/// </summary>
/// <param name="ConnectionString">Строка подключения</param>
/// <param name="DatabaseName">Имя базы данных</param>
/// <param name="CollectionName">Имя коллекции</param>
public record ConnectConfig(string ConnectionString, string DatabaseName, string CollectionName);

/// <summary>
/// Сервис
/// </summary>
public class Service : IService, IDisposable
{
    /// <summary>
    /// Конфигурация подключения к базе данных
    /// </summary>
    private readonly ConnectConfig _config;
    
    /// <summary>
    /// Строка подключения к базе данных
    /// </summary>
    private string ConnectionString => _config.ConnectionString;
    
    /// <summary>
    /// Имя базы данных
    /// </summary>
    private string DatabaseName => _config.DatabaseName;
    
    /// <summary>
    /// Имя коллекции
    /// </summary>
    private string CollectionName => _config.CollectionName;
    
    private readonly IMongoClient _client;
    
    /// <summary>
    /// Инициализация сервиса
    /// </summary>
    /// <param name="config">Конфигурация подключения</param>
    public Service(ConnectConfig config)
    {
        _config = config;

        _client = new MongoClient(ConnectionString);
    }
    
    /// <summary>
    /// Создать статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    public void Create(Article article) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .InsertOne(article);
        

    /// <summary>
    /// Обновить статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    public void Update(Article article) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .ReplaceOne(a => a.Id == article.Id, article);

    /// <summary>
    /// Удалить статью
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    public void Delete(Guid id) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .DeleteOne(a => a.Id == id);

    /// <summary>
    /// Получить все статьи
    /// </summary>
    /// <returns>Список статей</returns>
    public IEnumerable<Article> GetAll() => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(new BsonDocument())
            .ToList();

    /// <summary>
    /// Получить статью по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <returns>Статья</returns>
    public Article? GetBy(Guid id) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(a => a.Id == id)
            .SingleOrDefault();

    /// <summary>
    /// Получить статьи по тегам
    /// </summary>
    /// <param name="tags">Список тегов</param>
    /// <returns>Список статей</returns>
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags) => 
        _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .Find(a => a.Tags.Any(tags.Contains))
            .ToList();

    /// <summary>
    /// Освободить ресурсы
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }
}