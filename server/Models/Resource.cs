namespace server.Models;

public class Resource
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Url { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ResourceType Type { get; set; } = ResourceType.Other;
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } 

    //Relationships
    public Guid UserId { get; set; }
    public User User { get; set;  } = null!;
}