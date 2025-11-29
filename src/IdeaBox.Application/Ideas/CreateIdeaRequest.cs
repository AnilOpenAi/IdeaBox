namespace IdeaBox.Application.Ideas;

public class CreateIdeaRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    // İleride Tags vs. ekleriz
}