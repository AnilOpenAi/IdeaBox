using IdeaBox.Application.Common;
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

    public async Task<PagedResult<IdeaDto>> GetIdeasAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _dbContext
            .Ideas
            .AsNoTracking()
            .OrderByDescending(i => i.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(i => new IdeaDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Status = i.Status,
                CreatedAt = i.CreatedAt,
                VoteCount = i.Votes.Count   // Step 3 için property’yi şimdiden ekliyoruz
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<IdeaDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task LikeAsync(Guid ideaId, Guid userId, CancellationToken cancellationToken = default)
{
    var existing = await _dbContext.IdeaVotes
        .AnyAsync(v => v.IdeaId == ideaId && v.UserId == userId, cancellationToken);

    if (existing)
        return;

    var ideaExists = await _dbContext.Ideas
        .AnyAsync(i => i.Id == ideaId, cancellationToken);

    if (!ideaExists)
        throw new InvalidOperationException("Idea not found.");

    _dbContext.IdeaVotes.Add(new IdeaVote
    {
        IdeaId = ideaId,
        UserId = userId,
        CreatedAt = DateTime.UtcNow
    });

    await _dbContext.SaveChangesAsync(cancellationToken);
}

public async Task UnlikeAsync(Guid ideaId, Guid userId, CancellationToken cancellationToken = default)
{
    var vote = await _dbContext.IdeaVotes
        .SingleOrDefaultAsync(v => v.IdeaId == ideaId && v.UserId == userId, cancellationToken);

    if (vote == null)
        return;

    _dbContext.IdeaVotes.Remove(vote);
    await _dbContext.SaveChangesAsync(cancellationToken);
}
}