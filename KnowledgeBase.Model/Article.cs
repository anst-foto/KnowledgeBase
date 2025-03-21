// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace KnowledgeBase.Model;

public sealed partial class Article
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