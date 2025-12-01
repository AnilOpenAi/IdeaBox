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
public void Should_be_invalid_when_title_is_empty_or_whitespace(string title)
{
    var request = new CreateIdeaRequest
    {
        Title = title,
        Description = "Does not matter"
    };

    var result = _validator.Validate(request);

    Assert.False(result.IsValid);
    Assert.Contains(result.Errors, e => e.PropertyName == "Title");
}

[Fact]
public void Should_be_invalid_when_title_is_null()
{
    var request = new CreateIdeaRequest
    {
        Title = null!, // bilinçli olarak null veriyoruz
        Description = "Does not matter"
    };

    var result = _validator.Validate(request);

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
