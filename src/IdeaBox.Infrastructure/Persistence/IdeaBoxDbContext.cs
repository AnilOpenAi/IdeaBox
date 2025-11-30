using IdeaBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeaBox.Infrastructure.Persistence;

public class IdeaBoxDbContext : DbContext
{
    public IdeaBoxDbContext(DbContextOptions<IdeaBoxDbContext> options) : base(options)
    {
    }

    public DbSet<Idea> Ideas => Set<Idea>();
    public DbSet<User> Users => Set<User>();
    public DbSet<IdeaVote> IdeaVotes => Set<IdeaVote>();

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

        entity.HasOne(i => i.Owner)
              .WithMany(u => u.Ideas)
              .HasForeignKey(i => i.OwnerId)
              .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(u => u.Id);
        entity.HasIndex(u => u.Email).IsUnique();

        entity.Property(u => u.Email)
              .IsRequired()
              .HasMaxLength(256);

        entity.Property(u => u.PasswordHash)
              .IsRequired();
    });

    modelBuilder.Entity<IdeaVote>(entity =>
    {
        entity.HasKey(v => new { v.IdeaId, v.UserId });

        entity.HasOne(v => v.Idea)
              .WithMany(i => i.Votes)
              .HasForeignKey(v => v.IdeaId);

        entity.HasOne(v => v.User)
              .WithMany(u => u.Votes)
              .HasForeignKey(v => v.UserId);
    });
}
}