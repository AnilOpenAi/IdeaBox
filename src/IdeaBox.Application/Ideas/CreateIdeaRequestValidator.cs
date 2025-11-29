using FluentValidation;

namespace IdeaBox.Application.Ideas;

public class CreateIdeaRequestValidator : AbstractValidator<CreateIdeaRequest>
{
    public CreateIdeaRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title boş olamaz.")
            .MaximumLength(200).WithMessage("Title en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description boş olamaz.")
            .MinimumLength(10).WithMessage("Description en az 10 karakter olmalı.");
    }
}