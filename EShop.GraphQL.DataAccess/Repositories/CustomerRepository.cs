using EShop.GraphQL.DataAccess.Models;

namespace EShop.GraphQL.DataAccess.Repositories;

public class CustomerRepository : GenericRepository<Customer, AppDbContext>, ICustomerRepository
{
	public CustomerRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
