namespace server.Services;

using server.Data;
using server.Models;
using server.DTOs.Snippets;
using Microsoft.EntityFrameworkCore;

public class SnippetService
{
    private readonly AppDbContext _context;

    public SnippetService(AppDbContext context)
    {
        _context = context;
    }

    // GET ALL snippets for a user
    public async Task<List<SnippetResponseDto>> GetAllByUser(Guid userId)
    {
        return await _context.Snippets
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
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
    }

    // GET ONE snippet by id (with ownership check)
    public async Task<SnippetResponseDto?> GetById(Guid id, Guid userId)
    {
        var snippet = await _context.Snippets
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (snippet == null) return null;

        return new SnippetResponseDto
        {
            Id = snippet.Id,
            Title = snippet.Title,
            Description = snippet.Description,
            Code = snippet.Code,
            Language = snippet.Language,
            Tags = snippet.Tags,
            CreatedAt = snippet.CreatedAt,
            UpdatedAt = snippet.UpdatedAt
        };
    }

    // CREATE a new snippet
    public async Task<SnippetResponseDto> Create(CreateSnippetDto dto, Guid userId)
    {
        var snippet = new Snippet
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Code = dto.Code,
            Language = dto.Language,
            Tags = dto.Tags,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Snippets.AddAsync(snippet);
        await _context.SaveChangesAsync();

        return new SnippetResponseDto
        {
            Id = snippet.Id,
            Title = snippet.Title,
            Description = snippet.Description,
            Code = snippet.Code,
            Language = snippet.Language,
            Tags = snippet.Tags,
            CreatedAt = snippet.CreatedAt,
            UpdatedAt = snippet.UpdatedAt
        };
    }

    // UPDATE an existing snippet
    public async Task<SnippetResponseDto?> Update(Guid id, UpdateSnippetDto dto, Guid userId)
    {
        var snippet = await _context.Snippets
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (snippet == null) return null;

        snippet.Title = dto.Title;
        snippet.Description = dto.Description;
        snippet.Code = dto.Code;
        snippet.Language = dto.Language;
        snippet.Tags = dto.Tags;
        snippet.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new SnippetResponseDto
        {
            Id = snippet.Id,
            Title = snippet.Title,
            Description = snippet.Description,
            Code = snippet.Code,
            Language = snippet.Language,
            Tags = snippet.Tags,
            CreatedAt = snippet.CreatedAt,
            UpdatedAt = snippet.UpdatedAt
        };
    }

    // DELETE a snippet
    public async Task<bool> Delete(Guid id, Guid userId)
    {
        var snippet = await _context.Snippets
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (snippet == null) return false;

        _context.Snippets.Remove(snippet);
        await _context.SaveChangesAsync();

        return true;
    }
}