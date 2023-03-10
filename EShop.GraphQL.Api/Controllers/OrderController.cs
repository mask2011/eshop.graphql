using EShop.GraphQL.Api.Controllers.GenericController;
using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Repositories;

namespace EShop.GraphQL.Api.Controllers;

public class OrderController : CrudController<Order, IOrderRepository>
{
	protected override string EndpointName => "order";

	public override void DefineServices(IServiceCollection services)
	{
		services.AddScoped<IOrderRepository, OrderRepository>();
	}
}
