using FluentValidation;

namespace BuySell.Host.Validators.Images;

public class ImagesValidator : AbstractValidator<IFormFile>
{
    public ImagesValidator()
    {
        RuleFor(x => x)
            .Must(ValidationHelpers.ValidImage)
            .WithMessage("Slika mora biti u .png .jpg ili .jpeg formatu");
    }
}