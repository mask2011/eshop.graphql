using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Customer> Customer { get; set; }
	public DbSet<Order> Order { get; set; }
	public DbSet<Address> Address { get; set; }
	public DbSet<Product> Product { get; set; }
	public DbSet<OrderItem> OrderItem { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(AppDbContext).Assembly);
	}
}
