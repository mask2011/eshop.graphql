namespace EShop.GraphQL.DataAccess.Models;

public class OrderItem : Entity
{
	public Guid OrderId { get; set; }
	public virtual Order Order { get; set; }

	public Guid ProductId { get; set; }

	[UseSorting]
	public virtual Product Product { get; set; }
}
