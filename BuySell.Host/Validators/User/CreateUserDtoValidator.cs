using BuySell.Contracts.DTOs.User;
using BuySell.Data.Enums;
using FluentValidation;

namespace BuySell.Host.Validators.User
{
    public class CreateUserDtoValidator : AbstractValidator<UserCreateDto>
    {
        public CreateUserDtoValidator()
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

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Nije validna email adresa");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Polje 'UserName' ne sme da bude prazno");

            RuleFor(x => x.Firstname)
                .NotEmpty()
                .WithMessage("Polje 'Ime' ne sme da bude prazno");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Polje 'Prezime' ne sme da bude prazno");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Polje 'Role' ne sme da bude prazno")
                .Must(x => Enum.GetNames(typeof(RoleEnum)).Where(x => !x.Equals("Admin", StringComparison.OrdinalIgnoreCase)).ToList().Contains(x, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Role mora biti buyer ili seller");
        }
    }
}
