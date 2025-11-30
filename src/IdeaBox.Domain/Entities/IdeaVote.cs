namespace IdeaBox.Domain.Entities;

public class IdeaVote
{
    public Guid IdeaId { get; set; }
    public Idea Idea { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}