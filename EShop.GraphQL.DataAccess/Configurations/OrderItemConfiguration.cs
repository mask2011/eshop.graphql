using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.GraphQL.DataAccess.Configurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.ToTable(nameof(OrderItem));

		builder.HasOne(oi => oi.Order)
			.WithMany(o => o.OrderItems)
			.HasForeignKey(oi => oi.OrderId);

		builder.HasOne(oi => oi.Product)
			.WithMany()
			.HasForeignKey(oi => oi.ProductId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
