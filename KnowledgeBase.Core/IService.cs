using System.Collections.Generic;

using MongoDB.Bson;

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
    public void Create(Article article);
    
    /// <summary>
    /// Обновить статью
    /// </summary>
    /// <param name="article">Cтатья</param>
    public void Update(Article article);
    
    /// <summary>
    /// Удалить статью
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    public void Delete(ObjectId id);
    
    /// <summary>
    /// Получить все статьи
    /// </summary>
    /// <returns>Список статей</returns>
    public IEnumerable<Article> GetAll();
    
    /// <summary>
    /// Получить статью по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <returns>Статья</returns>
    public Article? GetBy(ObjectId id);
    
    /// <summary>
    /// Получить статьи по тегам
    /// </summary>
    /// <param name="tags">Список тегов</param>
    /// <returns>Список статей</returns>
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags);
}