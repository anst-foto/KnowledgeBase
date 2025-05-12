// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

/// <summary>
/// Интерфейс сервиса
/// </summary>
public interface IService
{
    /// <summary>
    /// Создать статью
    /// </summary>
    /// <param name="article">Статья</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task CreateAsync(Article article, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task UpdateAsync(Article article, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить статью
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить все статьи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список статей</returns>
    public IAsyncEnumerable<Article>? GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить статью по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Статья</returns>
    public Task<Article?> GetByAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить статьи по тегам
    /// </summary>
    /// <param name="tags">Список тегов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список статей</returns>
    public IAsyncEnumerable<Article>? GetByAsync(IEnumerable<string> tags,
        CancellationToken cancellationToken = default);
}