namespace server.DTOs.Tasks;

using server.Models;

public class TaskResponseDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public DevTaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public string Project { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}