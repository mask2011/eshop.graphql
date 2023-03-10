using EShop.GraphQL.DataAccess.Models;

namespace EShop.GraphQL.DataAccess.Repositories;

public class AddressRepository : GenericRepository<Address, AppDbContext>, IAddressRepository
{
	public AddressRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
