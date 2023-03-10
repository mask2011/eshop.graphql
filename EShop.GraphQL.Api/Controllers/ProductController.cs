using EShop.GraphQL.Api.Controllers.GenericController;
using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Repositories;

namespace EShop.GraphQL.Api.Controllers;

public class ProductController : CrudController<Product, IProductRepository>
{
	protected override string EndpointName => "products";

	public override void DefineServices(IServiceCollection services)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
	}
}
