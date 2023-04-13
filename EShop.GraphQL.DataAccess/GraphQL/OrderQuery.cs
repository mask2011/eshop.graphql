using EShop.GraphQL.DataAccess.Models;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.GraphQL;

[ExtendObjectType("Query")]
public class OrderQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> GetOrders([Service] AppDbContext context) =>
        context.Order;
}
