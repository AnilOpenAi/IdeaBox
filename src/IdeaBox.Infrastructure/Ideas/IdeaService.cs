using IdeaBox.Application.Ideas;
using IdeaBox.Domain.Entities;
using IdeaBox.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdeaBox.Infrastructure.Ideas;

public class IdeaService : IIdeaService
{
    private readonly IdeaBoxDbContext _dbContext;

    public IdeaService(IdeaBoxDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateIdeaAsync(CreateIdeaRequest request, Guid ownerId, CancellationToken cancellationToken = default)
    {
        var idea = new Idea
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            Title = request.Title,
            Description = request.Description,
            Status = IdeaStatus.Open,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Ideas.Add(idea);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return idea.Id;
    }

    public async Task<IReadOnlyList<IdeaDto>> GetIdeasAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Ideas
            .OrderByDescending(i => i.CreatedAt)
            .Select(i => new IdeaDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Status = i.Status,
                CreatedAt = i.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}