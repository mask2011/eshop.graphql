using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Schema.Inputs;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.Schema.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class OrderMutation
{
    public async Task<Order> CreateOrderAsync(
        OrderInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = input.CustomerId,
            AddressId = input.AddressId,
        };

        order.OrderItems = input.Products
            .Select(p => new OrderItem
            {
                Order = order,
                ProductId = p.Id
            })
            .ToList();

        order.Sum = input.Products.Sum(p => p.Price);

        await context.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Order> UpdateOrderAsync(
        Guid id,
        OrderInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var order = await context.Order.FindAsync(id, cancellationToken);

        if (order is null)
        {
            throw new GraphQLException(
                new Error("Order not found.", "ORDER_NOT_FOUND"));
        }

        order.CustomerId = input.CustomerId;
        order.AddressId = input.AddressId;

        var newOrderItems = input.Products
            .Where(p => !order.OrderItems
                .Select(oi => oi.ProductId).Contains(p.Id))
            .Select(p => new OrderItem
            {
                Order = order,
                ProductId = p.Id
            })
            .ToList();

        newOrderItems.ForEach(oi => order.OrderItems.Add(oi));

        order.Sum += input.Products.Sum(p => p.Price);

        await context.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<bool> DeleteOrderAsync(
        Guid id,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var order = await context.Order.FindAsync(id, cancellationToken);

        if (order is null)
        {
            throw new GraphQLException(
                new Error("Order not found.", "ORDER_NOT_FOUND"));
        }

        context.Remove(order);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
