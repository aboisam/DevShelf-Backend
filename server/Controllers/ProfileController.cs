using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using server.DTOs.Auth;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProfile()
    {
        // 1. Read claims from the authenticated user
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        // 2. Validate claims exist
        if (string.IsNullOrEmpty(userId) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(userName))
        {
            return Unauthorized(new { error = "Invalid token: missing required claims" });
        }

        // 3. Build and return the profile response
        return Ok(new ProfileResponseDto
        {
            Id = Guid.Parse(userId),
            Email = email,
            UserName = userName,
            FetchedAt = DateTime.UtcNow
        });
    }
}