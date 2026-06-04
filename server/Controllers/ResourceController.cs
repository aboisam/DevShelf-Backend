using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using server.Services;
using server.DTOs.Resources;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResourcesController : ControllerBase
{
    private readonly ResourceService _resourceService;

    public ResourcesController(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var resources = await _resourceService.GetAllByUser(userId);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        var resource = await _resourceService.GetById(id, userId);
        if (resource == null)
            return NotFound(new { error = "Resource not found" });
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceDto dto)
    {
        var userId = GetUserId();
        var resource = await _resourceService.Create(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResourceDto dto)
    {
        var userId = GetUserId();
        var result = await _resourceService.Update(id, dto, userId);
        if (result == null)
            return NotFound(new { error = "Resource not found" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _resourceService.Delete(id, userId);
        if (!success)
            return NotFound(new { error = "Resource not found" });
        return NoContent();
    }
}