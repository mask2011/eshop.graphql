using EShop.GraphQL.DataAccess.Models;

using HotChocolate;
using HotChocolate.Types;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess.Schema.Queries;

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> GetCustomers([Service] AppDbContext context) =>
        context.Customer;

    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Customer GetCustomerById([Service] AppDbContext context, Guid id) =>
        context.Customer
        .Include(c => c.Address)
        .Include(c => c.Orders)
        .ThenInclude(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .FirstOrDefault(c => c.Id == id);
}
