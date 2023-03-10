using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.GraphQL.DataAccess.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable(nameof(Order));

		builder.Property(o => o.Sum)
			.IsRequired();

		builder.HasOne(o => o.Address)
			.WithMany()
			.IsRequired();

		builder.HasOne(o => o.Customer)
			.WithMany(o => o.Orders)
			.IsRequired();
	}
}
