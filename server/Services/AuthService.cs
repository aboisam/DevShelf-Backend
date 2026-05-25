namespace server.Services;

using server.Models;
using server.DTOs.Auth;
using server.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _JwtService;

    public AuthService(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _JwtService = jwtService;
    }
    public async Task<(bool success, string message)> Register(RegisterDto dto)
    {
        var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null)
        {
            return (false, "Email already taken");
        }


        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
        };
        newUser.SetPassword(hashedPassword);

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return (true, "User registered successfully");

    }

    public async Task<(bool success, string message, AuthResponseDto? response)> Login(LoginDto dto)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return (false, "Invalid email or password", null);
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return (false, "Invalid email or password", null);
        }

        string token = _JwtService.GenerateToken(user);
        var response = new AuthResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = token
        };
        return (true, "Login successful", response);
    }
}




