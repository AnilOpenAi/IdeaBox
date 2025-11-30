using IdeaBox.Application.Common;

namespace IdeaBox.Application.Ideas;

public interface IIdeaService
{
    Task<Guid> CreateIdeaAsync(CreateIdeaRequest request, Guid ownerId, CancellationToken cancellationToken = default);

    Task<PagedResult<IdeaDto>> GetIdeasAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task LikeAsync(Guid ideaId, Guid userId, CancellationToken cancellationToken = default);
    Task UnlikeAsync(Guid ideaId, Guid userId, CancellationToken cancellationToken = default);
}