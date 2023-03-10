using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.GraphQL.DataAccess.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable(nameof(Product));

		builder.Property(p => p.Name)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(p => p.Description)
			.HasMaxLength(150)
			.IsRequired();

		builder.Property(p => p.Price)
			.IsRequired();
	}
}
