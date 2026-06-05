namespace server.Models;

public class DevTask
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public string Project { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Relationship
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}