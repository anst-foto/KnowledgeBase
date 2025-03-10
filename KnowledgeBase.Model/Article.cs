namespace KnowledgeBase.Model;

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