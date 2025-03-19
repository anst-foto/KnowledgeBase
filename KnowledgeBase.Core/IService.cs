using System.Collections.Generic;

using MongoDB.Bson;

using KnowledgeBase.Model;


namespace KnowledgeBase.Core;

public interface IService
{
    public void Create(Article article);
    public void Update(Article article);
    public void Delete(ObjectId id);
    public IEnumerable<Article> GetAll();
    public Article? GetBy(ObjectId id);
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags);
}