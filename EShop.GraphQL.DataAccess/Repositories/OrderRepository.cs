using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess.Repositories;

public class OrderRepository : GenericRepository<Order, AppDbContext>, IOrderRepository
{
	public OrderRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public override Task<Order> GetById(Guid id, IQueryable<Order> query = null)
	{
		if (query is not null)
		{
			return query.FirstOrDefaultAsync(p => p.Id == id);
		}

		return _dbContext.Set<Order>()
			.Include(o => o.Customer)
			.Include(o => o.Address)
			.Include(o => o.OrderItems)
			.FirstOrDefaultAsync(p => p.Id == id);
	}
}
