using EShop.GraphQL.Api.Controllers.GenericController;
using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Repositories;

namespace EShop.GraphQL.Api.Controllers;

public class CustomersController : CrudController<Customer, ICustomerRepository>
{
	protected override string EndpointName => "customers";

	public override void DefineServices(IServiceCollection services)
	{
		services.AddScoped<ICustomerRepository, CustomerRepository>();
	}
}
