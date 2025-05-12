// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

/// <summary>
/// Сервис
/// </summary>
public partial class Service : IService
{
    /// <summary>
    /// Создать статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task CreateAsync(Article article, CancellationToken cancellationToken = default)
    {
        await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .InsertOneAsync(
                article,
                null,
                cancellationToken);
    }


    /// <summary>
    /// Обновить статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task UpdateAsync(Article article, CancellationToken cancellationToken = default)
    {
        await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .ReplaceOneAsync(
                a => a.Id == article.Id,
                article,
                cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Удалить статью
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .DeleteOneAsync(
                a => a.Id == id,
                cancellationToken);
    }

    /// <summary>
    /// Получить все статьи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список статей</returns>
    public async IAsyncEnumerable<Article> GetAllAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cursor = await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .FindAsync(
                new BsonDocument(),
                cancellationToken: cancellationToken);
        while (await cursor.MoveNextAsync(cancellationToken))
            foreach (var current in cursor.Current)
                yield return current;
    }

    /// <summary>
    /// Получить статью по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Статья</returns>
    public async Task<Article?> GetByAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cursor = await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .FindAsync(
                a => a.Id == id,
                cancellationToken: cancellationToken);
        return await cursor.SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить статьи по тегам
    /// </summary>
    /// <param name="tags">Список тегов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список статей</returns>
    public async IAsyncEnumerable<Article>? GetByAsync(IEnumerable<string> tags,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cursor = await _client
            .GetDatabase(DatabaseName)
            .GetCollection<Article>(CollectionName)
            .FindAsync(
                a => a.Tags.Any(tags.Contains),
                cancellationToken: cancellationToken);
        while (await cursor.MoveNextAsync(cancellationToken))
            foreach (var current in cursor.Current)
                yield return current;
    }
}