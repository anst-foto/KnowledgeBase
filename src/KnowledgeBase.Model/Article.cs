// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;


namespace KnowledgeBase.Model;

/// <summary>
/// Статья
/// </summary>
public sealed partial class Article
{
    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Заголовок статьи
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Теги статьи
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Содержание статьи
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// Дата создания статьи
    /// </summary>
    public DateTime DateOfCreation { get; init; }


    /// <summary>
    /// Дата последнего обновления статьи
    /// </summary>
    public DateTime? DateOfLastUpdate { get; set; }


    /// <summary>
    /// Признак, что статья удалена или нет
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}