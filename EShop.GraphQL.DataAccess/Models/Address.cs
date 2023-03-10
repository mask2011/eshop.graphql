namespace EShop.GraphQL.DataAccess.Models;

public class Address : Entity
{
	public string Street { get; set; }
	public string Number { get; set; }
	public string ZipCode { get; set; }
	public string City { get; set; }

	public Guid CustomerId { get; set; }
	public Customer Customer { get; set; }
}
