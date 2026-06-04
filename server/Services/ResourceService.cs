namespace server.Services;

using server.Data;
using server.Models;
using server.DTOs.Resources;
using Microsoft.EntityFrameworkCore;

public class ResourceService
{
    private readonly AppDbContext _context;

    public ResourceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResourceResponseDto>> GetAllByUser(Guid userId)
    {
        return await _context.Resources
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
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
    }

    public async Task<ResourceResponseDto?> GetById(Guid id, Guid userId)  // int → Guid
    {
        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        if (resource == null) return null;

        return new ResourceResponseDto
        {
            Id = resource.Id,
            Title = resource.Title,
            Url = resource.Url,
            Notes = resource.Notes,
            Type = resource.Type,
            Tags = resource.Tags,
            CreatedAt = resource.CreatedAt
        };
    }

    public async Task<ResourceResponseDto> Create(CreateResourceDto dto, Guid userId)
    {
        var resource = new Resource
        {
            Title = dto.Title,
            Url = dto.Url,
            Notes = dto.Notes,
            Type = dto.Type,
            Tags = dto.Tags,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Resources.AddAsync(resource);
        await _context.SaveChangesAsync();

        return new ResourceResponseDto
        {
            Id = resource.Id,
            Title = resource.Title,
            Url = resource.Url,
            Notes = resource.Notes,
            Type = resource.Type,
            Tags = resource.Tags,
            CreatedAt = resource.CreatedAt
        };
    }

    public async Task<ResourceResponseDto?> Update(Guid id, UpdateResourceDto dto, Guid userId)  // int → Guid
    {
        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        if (resource == null) return null;

        resource.Title = dto.Title;
        resource.Url = dto.Url;
        resource.Notes = dto.Notes;
        resource.Type = dto.Type;
        resource.Tags = dto.Tags;

        await _context.SaveChangesAsync();

        return new ResourceResponseDto
        {
            Id = resource.Id,
            Title = resource.Title,
            Url = resource.Url,
            Notes = resource.Notes,
            Type = resource.Type,
            Tags = resource.Tags,
            CreatedAt = resource.CreatedAt
        };
    }

    public async Task<bool> Delete(Guid id, Guid userId)  // int → Guid
    {
        var resource = await _context.Resources
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        if (resource == null) return false;

        _context.Resources.Remove(resource);
        await _context.SaveChangesAsync();

        return true;
    }
}