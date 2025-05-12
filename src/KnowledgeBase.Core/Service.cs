// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace KnowledgeBase.Core;

public partial class Service
{
    #region ConnectConfig

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

    #endregion

    private readonly MongoClient _client;

    /// <summary>
    /// Инициализация сервиса
    /// </summary>
    /// <param name="config">Конфигурация подключения</param>
    public Service(ConnectConfig config)
    {
        _config = config;

        _client = new MongoClient(ConnectionString);
        
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    ~Service()
    {
        Dispose(false);
    }
}