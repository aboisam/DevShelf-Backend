namespace server.DTOs.Tasks;

using server.Models;

public class CreateTaskDto
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public string Project { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
}