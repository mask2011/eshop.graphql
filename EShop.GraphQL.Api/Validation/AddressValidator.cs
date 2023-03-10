using EShop.GraphQL.DataAccess.Models;

using FluentValidation;

namespace EShop.GraphQL.Api.Validation;

public class AddressValidator : AbstractValidator<Address>
{
	public AddressValidator()
	{
		RuleFor(a => a.City)
			.NotNull().WithMessage("The 'City' field is required")
			.NotEmpty().WithMessage("The 'City' field is required")
			.MaximumLength(80).WithMessage("The 'City' cannot be more than 80 characters");

		RuleFor(a => a.Number)
			.NotNull().WithMessage("The 'Number' field is required")
			.NotEmpty().WithMessage("The 'Number' field is required");

		RuleFor(a => a.ZipCode)
			.NotNull().WithMessage("The 'ZipCode' field is required")
			.NotEmpty().WithMessage("The 'ZipCode' field is required");

		RuleFor(a => a.Street)
			.NotNull().WithMessage("The 'Street' field is required")
			.NotEmpty().WithMessage("The 'Street' field is required")
			.MaximumLength(80).WithMessage("The 'Street' cannot be more than 80 characters");
	}
}
