using IdeaBox.Application.Common;
using IdeaBox.Application.Ideas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdeaBox.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Step 2’de JWT eklediğimizde devreye girecek
public class IdeasController : ControllerBase
{
    private readonly IIdeaService _ideaService;

    public IdeasController(IIdeaService ideaService)
    {
        _ideaService = ideaService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIdea(
        [FromBody] CreateIdeaRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId == null)
            return Unauthorized();

        var id = await _ideaService.CreateIdeaAsync(request, userId.Value, cancellationToken);

        return Created($"/api/ideas/{id}", new { id });
    }

    [AllowAnonymous] // Şimdilik listelemeyi herkese açık bırakabiliriz
    [HttpGet]
    public async Task<ActionResult<PagedResult<IdeaDto>>> GetIdeas(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _ideaService.GetIdeasAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    private Guid? GetUserId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (idClaim == null)
            return null;

        if (Guid.TryParse(idClaim.Value, out var id))
            return id;

        return null;
    }

    [HttpPost("{id:guid}/like")]
public async Task<IActionResult> Like(Guid id, CancellationToken cancellationToken)
{
    var userId = GetUserId();
    if (userId == null)
        return Unauthorized();

    await _ideaService.LikeAsync(id, userId.Value, cancellationToken);
    return NoContent();
}

[HttpPost("{id:guid}/unlike")]
public async Task<IActionResult> Unlike(Guid id, CancellationToken cancellationToken)
{
    var userId = GetUserId();
    if (userId == null)
        return Unauthorized();

    await _ideaService.UnlikeAsync(id, userId.Value, cancellationToken);
    return NoContent();
}
}