using EShop.GraphQL.DataAccess.Models;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.GraphQL;

[ExtendObjectType("Query")]
public class ProductQuery
{
	[UseProjection]
	[UseFiltering]
	[UseSorting]
	public IQueryable<Product> GetProducts([Service] AppDbContext context) =>
		context.Product;
}
