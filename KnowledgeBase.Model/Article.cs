using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace KnowledgeBase.Model;

/// <summary>
/// Модель статьи
/// </summary>
public partial class Article
{
    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    [BsonId]
    public ObjectId Id { get; init; }
    
    /// <summary>
    /// Заголовок статьи
    /// </summary>
    [BsonElement("title")]
    public required string Title { get; set; }
    
    /// <summary>
    /// Теги статьи
    /// </summary>
    [BsonElement("tags")]
    public List<string> Tags { get; set; } = [];
    
    /// <summary>
    /// Содержание статьи
    /// </summary>
    [BsonElement("content")]
    public required string Content { get; set; }
    
    /// <summary>
    /// Дата создания статьи
    /// </summary>
    [BsonElement("date_of_creation")]
    public DateTime DateOfCreation { get; init; }
    
    
    /// <summary>
    /// Дата последнего обновления статьи
    /// </summary>
    [BsonElement("date_of_update")]
    public DateTime DateOfLastUpdate { get; set; } //TODO Сделать nullable
    
    
    /// <summary>
    /// Признак что статья удалена или нет
    /// </summary>
    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}