using BuyAndSell.Contracts.DTOs.User;
using FluentValidation;

namespace BuySell.Host.Validators.User
{
    public class ChangePasswordUserDtoValidator : AbstractValidator<UserChangePasswordDto>
    {
        public ChangePasswordUserDtoValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Minimalna dužina šifre je 6 karaktera")
                .Must(ValidationHelpers.HasLowercase)
                .WithMessage("Šifra mora da sadrži najmanje jedno malo slovo")
                .Must(ValidationHelpers.HasUppercase)
                .WithMessage("Šifra mora da sadrži najmanje jedno veliko slovo")
                .Must(ValidationHelpers.HasSymbol)
                .WithMessage("Šifra mora da sadrži najmanje jedan simbol")
                .Must(ValidationHelpers.HasDigit)
                .WithMessage("Šifra mora da sadrži najmanje jedan broj");
        }
    }
}
