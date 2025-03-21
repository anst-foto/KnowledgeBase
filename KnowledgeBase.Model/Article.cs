// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace KnowledgeBase.Model;

public partial class Article
{
    [BsonId]
    public ObjectId Id { get; init; }
    
    [BsonElement("title")]
    public required string Title { get; set; }
    
    [BsonElement("tags")]
    public List<string> Tags { get; set; } = [];
    
    [BsonElement("content")]
    public required string Content { get; set; }
    
    [BsonElement("date_of_creation")]
    public DateTime DateOfCreation { get; init; }
    
    [BsonElement("date_of_update")]
    public DateTime DateOfLastUpdate { get; set; } //TODO Сделать nullable
    
    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}