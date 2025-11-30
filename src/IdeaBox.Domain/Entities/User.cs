namespace IdeaBox.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Idea> Ideas { get; set; } = new List<Idea>();
    public ICollection<IdeaVote> Votes { get; set; } = new List<IdeaVote>();
}