using EShop.GraphQL.DataAccess.Models;

using HotChocolate;
using HotChocolate.Types;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess.Schema.Queries;

[ExtendObjectType(typeof(Query))]
public class OrderQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> GetOrders([Service] AppDbContext context) =>
        context.Order;

    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Order GetOrderById([Service] AppDbContext context, Guid id) =>
       context.Order
       .Include(c => c.Address)
       .Include(o => o.OrderItems)
       .ThenInclude(oi => oi.Product)
       .FirstOrDefault(c => c.Id == id);
}
