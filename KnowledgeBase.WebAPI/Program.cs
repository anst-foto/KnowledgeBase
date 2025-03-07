using KnowledgeBase.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

IService service;

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

app.Run();