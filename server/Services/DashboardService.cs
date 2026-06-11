namespace server.Services;

using server.Data;
using server.Models;
using server.DTOs.Dashboard;
using server.DTOs.Snippets;
using server.DTOs.Resources;
using server.DTOs.Tasks;
using Microsoft.EntityFrameworkCore;

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDto> GetStats(Guid userId)
    {
        // Count totals
        var totalSnippets = await _context.Snippets
            .CountAsync(s => s.UserId == userId);

        var totalResources = await _context.Resources
            .CountAsync(r => r.UserId == userId);

        var totalTasks = await _context.Tasks
            .CountAsync(t => t.UserId == userId);

        // Count tasks by status
        var tasksTodo = await _context.Tasks
            .CountAsync(t => t.UserId == userId && t.Status == DevTaskStatus.ToDo);

        var tasksInProgress = await _context.Tasks
            .CountAsync(t => t.UserId == userId && t.Status == DevTaskStatus.InProgress);

        var tasksDone = await _context.Tasks
            .CountAsync(t => t.UserId == userId && t.Status == DevTaskStatus.Done);

        // Get 5 most recent of each
        var recentSnippets = await _context.Snippets
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .Take(5)
            .Select(s => new SnippetResponseDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Code = s.Code,
                Language = s.Language,
                Tags = s.Tags,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .ToListAsync();

        var recentResources = await _context.Resources
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .Take(5)
            .Select(r => new ResourceResponseDto
            {
                Id = r.Id,
                Title = r.Title,
                Url = r.Url,
                Notes = r.Notes,
                Type = r.Type,
                Tags = r.Tags,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();

        var recentTasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Take(5)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Project = t.Project,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();

        return new DashboardStatsDto
        {
            TotalSnippets = totalSnippets,
            TotalResources = totalResources,
            TotalTasks = totalTasks,
            TasksTodo = tasksTodo,
            TasksInProgress = tasksInProgress,
            TasksDone = tasksDone,
            RecentSnippets = recentSnippets,
            RecentResources = recentResources,
            RecentTasks = recentTasks
        };
    }
}