namespace server.DTOs.Snippets;

public class CreateSnippetDto
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string Code { get; set; }
    public required string Language { get; set; }
    public List<string> Tags { get; set; } = new();
}