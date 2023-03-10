using EShop.GraphQL.DataAccess.Models;

using FluentValidation;

namespace EShop.GraphQL.Api.Validation;

public class ProductValidator : AbstractValidator<Product>
{
	public ProductValidator()
	{
		RuleFor(a => a.Name)
			.NotNull().WithMessage("The 'Name' field is required")
			.NotEmpty().WithMessage("The 'Name' field is required")
			.MaximumLength(100).WithMessage("The 'Name' cannot be more than 100 characters");

		RuleFor(a => a.Description)
			.NotNull().WithMessage("The 'Description' field is required")
			.NotEmpty().WithMessage("The 'Description' field is required")
			.MaximumLength(150).WithMessage("The 'Description' cannot be more than 150 characters");

		RuleFor(a => a.Price)
			.GreaterThan(0).WithMessage("The 'Price' field must be above 0")
			.NotNull().WithMessage("The 'Price' field is required");
	}
}
