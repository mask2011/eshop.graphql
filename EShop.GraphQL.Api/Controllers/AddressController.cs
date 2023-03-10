using EShop.GraphQL.Api.Controllers.GenericController;
using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Repositories;

namespace EShop.GraphQL.Api.Controllers;

public class AddressController : CrudController<Address, IAddressRepository>
{
	protected override string EndpointName => "address";

	public override void DefineServices(IServiceCollection services)
	{
		services.AddScoped<IAddressRepository, AddressRepository>();
	}
}
