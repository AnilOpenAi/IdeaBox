using IdeaBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeaBox.Infrastructure.Persistence;

public class IdeaBoxDbContext : DbContext
{
    public IdeaBoxDbContext(DbContextOptions<IdeaBoxDbContext> options) : base(options)
    {
    }

    public DbSet<Idea> Ideas => Set<Idea>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Idea>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(i => i.Description)
                  .IsRequired();
        
        });
    }
}