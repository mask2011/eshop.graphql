using EShop.GraphQL.DataAccess.Models;

namespace EShop.GraphQL.DataAccess.Repositories;

public class ProductRepository : GenericRepository<Product, AppDbContext>, IProductRepository
{
	public ProductRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
