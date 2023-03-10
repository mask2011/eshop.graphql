namespace Eshop.GraphQL.Api.Core.Endpoints;

public interface IEndpointDefinition
{
	void DefineServices(IServiceCollection services);

	void DefineEndpoints(WebApplication app);
}
