using Microsoft.AspNetCore.Identity;

namespace server.Models;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string PasswordHash { get; private set; } = String.Empty;

    public void SetPassword(string hash)
    {
        PasswordHash = hash;
    }

    public DateTime CreatedAt { get; set; }

    public List<Resource> Resources { get; set; } = new();
}

