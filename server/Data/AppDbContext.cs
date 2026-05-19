namespace server.Data;

using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using server.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
}
