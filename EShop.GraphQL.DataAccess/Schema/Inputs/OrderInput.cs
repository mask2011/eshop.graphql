namespace EShop.GraphQL.DataAccess.Schema.Inputs;

public record OrderInput(Guid CustomerId, Guid AddressId, List<OrderProductInput> Products);
