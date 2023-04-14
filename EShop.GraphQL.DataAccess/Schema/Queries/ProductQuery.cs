using EShop.GraphQL.DataAccess.Models;

using HotChocolate;
using HotChocolate.Types;

namespace EShop.GraphQL.DataAccess.Schema.Queries;

[ExtendObjectType("Query")]
public class ProductQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts([Service] AppDbContext context) =>
        context.Product;

    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public Product GetProductById([Service] AppDbContext context, Guid id) =>
       context.Product
       .FirstOrDefault(c => c.Id == id);
}
