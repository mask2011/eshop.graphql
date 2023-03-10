using EShop.GraphQL.DataAccess.Models;

using HotChocolate;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess.GraphQL;

public class Query
{
	[UseProjection]
	[UseFiltering]
	[UseSorting]
	public IQueryable<Customer> GetCustomers([Service] AppDbContext context) =>
		context.Customer;

	[UseProjection]
	[UseFiltering]
	public Customer GetCustomerById([Service] AppDbContext context, Guid id) =>
		context.Customer
		.Include(c => c.Address)
		.Include(c => c.Orders)
		.ThenInclude(o => o.OrderItems)
		.ThenInclude(oi => oi.Product)
		.FirstOrDefault(c => c.Id == id);
}
