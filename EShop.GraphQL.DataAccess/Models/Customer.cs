namespace EShop.GraphQL.DataAccess.Models;

public class Customer : Entity
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }

	public Address Address { get; set; }

	[UseSorting]
	public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
