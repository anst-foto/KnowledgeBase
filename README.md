[![CodeFactor](https://www.codefactor.io/repository/github/anst-foto/knowledgebase/badge)](https://www.codefactor.io/repository/github/anst-foto/knowledgebase)

# База знаний

## Модель данных

```csharp
public class Article
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public List<string> Tags { get; set; } = [];
    public required string Content { get; set; }
    public DateTime DateOfCreation { get; init; }
    public DateTime DateOfLastUpdate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
```

```csharp
public interface IService
{
    public void Create(Article article);
    public void Update(Article article);
    public void Delete(Guid id);
    public IEnumerable<Article> GetAll();
    public Article? GetBy(Guid id);
    public IEnumerable<Article>? GetBy(IEnumerable<string> tags);
}
```

![diagram_class-0.png](doc/diagram_class-0.png)

## WebAPI
```csharp
const string BASE_URL = "/api/v0";

app.MapGet($"{BASE_URL}/articles", () => service.GetAll());
app.MapGet($"{BASE_URL}/articles/{{id:guid}}", (Guid id) => service.GetBy(id));
app.MapGet($"{BASE_URL}/articles/tags", (HttpContext context) =>
{
    var tags = context.Request.Query["tags"].ToList();
    return service.GetBy(tags);
});
app.MapPost($"{BASE_URL}/articles", (Article article) => service.Create(article));
app.MapPut($"{BASE_URL}/articles", (Article article) => service.Update(article));
app.MapDelete($"{BASE_URL}/articles/{{id:guid}}", (Guid id) => service.Delete(id));

```