using IdeaBox.Application.Ideas;
using IdeaBox.Infrastructure.Ideas;
using IdeaBox.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdeaBoxDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

// Application services
builder.Services.AddScoped<IIdeaService, IdeaService>();

// TODO: Auth/JWT ileride eklenecek
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();