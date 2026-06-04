// CreateResourceDto.cs
namespace server.DTOs.Resources;

using server.Models;

public class CreateResourceDto  // removed the 's'
{
    public required string Title { get; set; }
    public required string Url { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ResourceType Type { get; set; } = ResourceType.Other;
    public List<string> Tags { get; set; } = new();
}