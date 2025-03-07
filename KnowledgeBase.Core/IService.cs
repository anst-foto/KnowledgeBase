namespace KnowledgeBase.Core;

public interface IService
{
    public void Create(Article article);
    public void Update(Article article);
    public void Delete(Guid id);
    public IEnumerable<Article> GetAll();
    public Article? GetBy(Guid id);
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags);
}