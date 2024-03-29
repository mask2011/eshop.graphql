﻿using FluentValidation.Results;

namespace Eshop.GraphQL.Api.Core.Extensions;

public static class ValidationExtensions
{
	public static IDictionary<string, string[]> ToDictionary(
		this ValidationResult validationResult)
			=> validationResult.Errors
					.GroupBy(x => x.PropertyName)
					.ToDictionary(
						g => g.Key,
						g => g.Select(x => x.ErrorMessage).ToArray());
}
