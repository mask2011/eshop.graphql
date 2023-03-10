namespace EShop.GraphQL.DataAccess.Models;

public class Order : Entity
{
	public decimal Sum { get; set; }

	public Guid CustomerId { get; set; }
	public Customer Customer { get; set; }

	public Guid AddressId { get; set; }
	public Address Address { get; set; }

	[UseSorting]
	public ICollection<OrderItem> OrderItems { get; set; } =
		new HashSet<OrderItem>();
}
