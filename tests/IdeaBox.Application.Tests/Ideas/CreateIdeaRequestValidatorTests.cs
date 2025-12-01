using IdeaBox.Application.Ideas;
using Xunit;

namespace IdeaBox.Application.Tests.Ideas;

public class CreateIdeaRequestValidatorTests
{
    private readonly CreateIdeaRequestValidator _validator = new();

    [Fact]
    public void Should_be_valid_when_title_is_given()
    {
        // arrange
        var request = new CreateIdeaRequest
        {
            Title = "Test idea",
            Description = "Some description"
        };

        // act
        var result = _validator.Validate(request);

        // assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_be_invalid_when_title_is_missing(string? title)
    {
        // arrange
        var request = new CreateIdeaRequest
        {
            Title = title,
            Description = "Does not matter"
        };

        // act
        var result = _validator.Validate(request);

        // assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Should_be_invalid_when_title_exceeds_max_length()
    {
        // arrange
        var longTitle = new string('x', 201); // Validator'da 200 limit olduğunu varsayıyoruz
        var request = new CreateIdeaRequest
        {
            Title = longTitle,
            Description = "Some description"
        };

        // act
        var result = _validator.Validate(request);

        // assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }
}
