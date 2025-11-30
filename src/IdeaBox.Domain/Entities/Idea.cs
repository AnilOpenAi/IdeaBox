namespace IdeaBox.Domain.Entities;

public class Idea
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public IdeaStatus Status { get; set; } = IdeaStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? Owner { get; set; }
    public ICollection<IdeaVote> Votes { get; set; } = new List<IdeaVote>();
}