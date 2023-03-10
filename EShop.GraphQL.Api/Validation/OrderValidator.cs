using EShop.GraphQL.DataAccess.Models;

using FluentValidation;

namespace EShop.GraphQL.Api.Validation;

public class OrderValidator : AbstractValidator<Order>
{
	public OrderValidator()
	{
		RuleFor(a => a.CustomerId)
			.NotNull().WithMessage("The 'Customer' field is required");

		RuleFor(a => a.AddressId)
			.NotNull().WithMessage("The 'Address' field is required");
	}
}
