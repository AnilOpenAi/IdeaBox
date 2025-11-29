namespace IdeaBox.Application.Ideas;

public interface IIdeaService
{
    Task<Guid> CreateIdeaAsync(CreateIdeaRequest request, Guid ownerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<IdeaDto>> GetIdeasAsync(CancellationToken cancellationToken = default);
}