using BuySell.Contracts.DTOs.Order;
using FluentValidation;
using System.Net;

namespace BuySell.Host.Validators.Order
{
    public class AddOrderItemValidator : AbstractValidator<AddOrderItemDto>
    {
        public AddOrderItemValidator()
        {
            RuleFor(x => x.Items)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("Lista ima duplikate itema")
                .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
