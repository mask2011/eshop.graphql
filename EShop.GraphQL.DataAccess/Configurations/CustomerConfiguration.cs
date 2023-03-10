using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.GraphQL.DataAccess.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.ToTable(nameof(Customer));

		builder.Property(c => c.FirstName)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(c => c.FirstName)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(c => c.LastName)
			.HasMaxLength(50)
			.IsRequired();

		builder.Property(c => c.Email)
			.HasMaxLength(80)
			.IsRequired();

		builder.HasOne(c => c.Address)
			.WithOne(a => a.Customer);

		builder.HasMany(c => c.Orders)
			.WithOne(o => o.Customer)
			.HasForeignKey(o => o.CustomerId);
	}
}
