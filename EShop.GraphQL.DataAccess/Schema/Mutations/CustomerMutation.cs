using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Schema.Inputs;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.Schema.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class CustomerMutation
{
    public async Task<Customer> CreateCustomerAsync(
        CustomerInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var address = await context.Address.FindAsync(
            input.AddressId,
            cancellationToken);

        var customer = new Customer
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            Address = address,
        };

        await context.AddAsync(customer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(
        Guid id,
        CustomerInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var customer = await context.Customer.FindAsync(id, cancellationToken);

        if (customer is null)
        {
            throw new GraphQLException(
                new Error("Customer not found.", "CUSTOMER_NOT_FOUND"));
        }

        var address = await context.Address.FindAsync(
            input.AddressId,
            cancellationToken);

        if (address is null)
        {
            throw new GraphQLException(
                new Error("Address not found.", "ADDRESS_NOT_FOUND"));
        }

        customer.FirstName = input.FirstName;
        customer.LastName = input.LastName;
        customer.Email = input.Email;
        customer.Address = address;

        await context.SaveChangesAsync(cancellationToken);

        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(
        Guid id,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var customer = await context.Customer.FindAsync(id, cancellationToken);

        if (customer is null)
        {
            throw new GraphQLException(
               new Error("Customer not found.", "CUSTOMER_NOT_FOUND"));
        }

        context.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
