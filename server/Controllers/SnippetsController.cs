using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using server.Services;
using server.DTOs.Snippets;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SnippetsController : ControllerBase
{
    private readonly SnippetService _snippetService;

    public SnippetsController(SnippetService snippetService)
    {
        _snippetService = snippetService;
    }

    // Helper: Extract user ID from JWT claims
    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    // GET /api/snippets
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var snippets = await _snippetService.GetAllByUser(userId);
        return Ok(snippets);
    }

    // GET /api/snippets/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        var snippet = await _snippetService.GetById(id, userId);

        if (snippet == null)
            return NotFound(new { error = "Snippet not found" });

        return Ok(snippet);
    }

    // POST /api/snippets
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSnippetDto dto)
    {
        var userId = GetUserId();
        var snippet = await _snippetService.Create(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = snippet.Id }, snippet);
    }

    // PUT /api/snippets/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSnippetDto dto)
    {
        var userId = GetUserId();
        var result = await _snippetService.Update(id, dto, userId);

        if (result == null)
            return NotFound(new { error = "Snippet not found" });

        return Ok(result);
    }

    // DELETE /api/snippets/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        var success = await _snippetService.Delete(id, userId);

        if (!success)
            return NotFound(new { error = "Snippet not found" });

        return NoContent();
    }
}