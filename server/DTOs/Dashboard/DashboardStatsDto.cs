namespace server.DTOs.Dashboard;

using server.DTOs.Snippets;
using server.DTOs.Resources;
using server.DTOs.Tasks;

public class DashboardStatsDto
{
    public int TotalSnippets { get; set; }
    public int TotalResources { get; set; }
    public int TotalTasks { get; set; }
    public int TasksTodo { get; set; }
    public int TasksInProgress { get; set; }
    public int TasksDone { get; set; }
    public List<SnippetResponseDto> RecentSnippets { get; set; } = new();
    public List<ResourceResponseDto> RecentResources { get; set; } = new();
    public List<TaskResponseDto> RecentTasks { get; set; } = new();
}