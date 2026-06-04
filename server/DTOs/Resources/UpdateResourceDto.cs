namespace server.DTOs.Resources;

using server.Models;
public class UpdateResourceDto
{
    public required string Title { get; set; }
    public required string Url { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ResourceType Type { get; set; } = ResourceType.Other;
    public List<string> Tags { get; set; } = new();
}