using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=devshelf.db"));


var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/", () => "Devshelf API is running...");

app.Run();

