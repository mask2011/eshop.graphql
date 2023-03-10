using EShop.GraphQL.DataAccess.Models;

using FluentValidation;

namespace EShop.GraphQL.Api.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
	public CustomerValidator()
	{
		RuleFor(a => a.FirstName)
			.NotNull().WithMessage("The 'FirstName' field is required")
			.NotEmpty().WithMessage("The 'FirstName' field is required")
			.MaximumLength(50).WithMessage("The 'FirstName' cannot be more than 50 characters");

		RuleFor(a => a.LastName)
			.NotNull().WithMessage("The 'LastName' field is required")
			.NotEmpty().WithMessage("The 'LastName' field is required")
			.MaximumLength(50).WithMessage("The 'LastName' cannot be more than 50 characters");

		RuleFor(a => a.Email)
			.NotNull().WithMessage("The 'City' field is required")
			.NotEmpty().WithMessage("The 'City' field is required")
			.EmailAddress().WithMessage("The 'Email' field is not a valid email address")
			.MaximumLength(80).WithMessage("The 'City' cannot be more than 80 characters");
	}
}
