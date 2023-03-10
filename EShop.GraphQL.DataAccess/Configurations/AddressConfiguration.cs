using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.GraphQL.DataAccess.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.ToTable(nameof(Address));

		builder.Property(a => a.Number)
			.IsRequired();

		builder.Property(a => a.ZipCode)
			.IsRequired();

		builder.Property(a => a.Street)
			.HasMaxLength(80)
			.IsRequired();

		builder.Property(a => a.City)
			.HasMaxLength(80)
			.IsRequired();
	}
}
