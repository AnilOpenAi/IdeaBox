using IdeaBox.Domain.Entities;

namespace IdeaBox.Application.Ideas;

public class IdeaDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IdeaStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int VoteCount { get; set; }

}