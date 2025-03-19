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
/// Сервис для работы с базой данных MongoDB
/// </summary>
public class Service : IService
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
    
    /// <summary>
    /// Коллекция
    /// </summary>
    private readonly IMongoCollection<Article> _collection;
    
    /// <summary>
    /// Инициализация сервиса
    /// </summary>
    /// <param name="config">Конфигурация подключения</param>
    public Service(ConnectConfig config)
    {
        _config = config;

        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        _collection = db.GetCollection<Article>(CollectionName);
    }
    
    /// <summary>
    /// Создать статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    public void Create(Article article) => 
        _collection.InsertOne(article);

    /// <summary>
    /// Обновить статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    public void Update(Article article) => 
        _collection.ReplaceOne(a => a.Id == article.Id, article);

    /// <summary>
    /// Удалить статью
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    public void Delete(ObjectId id) => 
        _collection.DeleteOne(a => a.Id == id);

    /// <summary>
    /// Получить все статьи
    /// </summary>
    /// <returns>Список статей</returns>
    public IEnumerable<Article> GetAll() => 
        _collection
            .Find(new BsonDocument())
            .ToList();

    /// <summary>
    /// Получить статью по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <returns>Статья</returns>
    public Article? GetBy(ObjectId id) => 
        _collection
            .Find(a => a.Id == id)
            .SingleOrDefault();

    /// <summary>
    /// Получить статьи по тегам
    /// </summary>
    /// <param name="tags">Список тегов</param>
    /// <returns>Список статей</returns>
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags) => 
        _collection
            .Find(a => a.Tags.Any(tags.Contains))
            .ToList();
}