using IdeaBox.Application.Ideas;
using Microsoft.AspNetCore.Mvc;

namespace IdeaBox.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdeasController : ControllerBase
{
    private readonly IIdeaService _ideaService;

    public IdeasController(IIdeaService ideaService)
    {
        _ideaService = ideaService;
    }

    // Şimdilik ownerId sabit, auth ekleyince user'dan alacağız
    private static readonly Guid FakeUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    [HttpPost]
    public async Task<IActionResult> CreateIdea([FromBody] CreateIdeaRequest request, CancellationToken cancellationToken)
    {
        var id = await _ideaService.CreateIdeaAsync(request, FakeUserId, cancellationToken);
        return CreatedAtAction(nameof(GetIdeas), new { id }, new { id });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdeaDto>>> GetIdeas(CancellationToken cancellationToken)
    {
        var ideas = await _ideaService.GetIdeasAsync(cancellationToken);
        return Ok(ideas);
    }
}