namespace EShop.GraphQL.DataAccess.Schema.Inputs;

public record CustomerInput(string FirstName, string LastName, string Email, Guid AddressId);
