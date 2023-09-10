using BuySell.Contracts.DTOs.Order;
using FluentValidation;
using System.Net;

namespace BuySell.Host.Validators.Order
{
    public class RemoveOrderItemValidator : AbstractValidator<RemoveOrderItemFromOrderDto>
    {
        public RemoveOrderItemValidator()
        {
            RuleFor(x => x.Items)
               .Must(x => x.Distinct().Count() == x.Count)
               .WithMessage("Lista ima duplikate itema")
               .WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
