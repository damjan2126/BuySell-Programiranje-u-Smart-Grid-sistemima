using BuySell.Contracts.DTOs.User;
using FluentValidation;

namespace BuySell.Host.Validators.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Polje 'Ime' ne sme da bude prazno");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Polje 'Prezime' ne sme da bude prazno");
        }
    }
}
