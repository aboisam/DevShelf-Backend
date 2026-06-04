namespace server.DTOs.Resources;

using server.Models;

public class ResourceResponseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Url { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ResourceType Type { get; set; }
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}