using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Schema.Inputs;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.Schema.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class ProductMutation
{
    public async Task<Product> CreateProduct(
        ProductInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price
        };

        await context.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateProduct(
        Guid id,
        ProductInput input,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var product = await context.Product.FindAsync(id, cancellationToken);

        if (product is null)
        {
            throw new GraphQLException(
                 new Error("Product not found.", "PRODUCT_NOT_FOUND"));
        }

        product.Name = input.Name;
        product.Description = input.Description;
        product.Price = input.Price;

        await context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> DeleteProduct(
        Guid id,
        [Service] AppDbContext context,
        CancellationToken cancellationToken)
    {
        var product = await context.Product.FindAsync(id, cancellationToken);

        if (product is null)
        {
            throw new GraphQLException(
                new Error("Product not found.", "PRODUCT_NOT_FOUND"));
        }

        context.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
