using Bogus;

using EShop.GraphQL.DataAccess.Models;

namespace EShop.GraphQL.DataAccess;

public static class DataSeed
{
	public static void Seed(AppDbContext context)
	{
		var customers = GetCustomers(3);

		var products = GetProducts(30);

		var orders = GetOrders(10, customers, products);

		context.Customer.AddRange(customers);
		context.Order.AddRange(orders);
		context.Product.AddRange(products);

		context.SaveChanges();
	}

	private static List<Customer> GetCustomers(int numberToGenerate) =>
		new Faker<Customer>()
			.RuleFor(c => c.Id, _ => Guid.NewGuid())
			.RuleFor(c => c.FirstName, (f, _) => f.Person.FirstName)
			.RuleFor(c => c.LastName, (f, _) => f.Person.LastName)
			.RuleFor(c => c.Email, (f, _) => f.Internet.Email())
			.RuleFor(c => c.Address, (_, c) => GetAddress(c.Id))
			.Generate(numberToGenerate);

	private static Address GetAddress(Guid customerId) =>
		new Faker<Address>()
			.RuleFor(a => a.Id, _ => Guid.NewGuid())
			.RuleFor(a => a.Street, (f, _) => f.Address.StreetName())
			.RuleFor(a => a.Number, (f, _) => f.Address.BuildingNumber())
			.RuleFor(a => a.ZipCode, (f, _) => f.Address.ZipCode())
			.RuleFor(a => a.City, (f, _) => f.Address.City())
			.RuleFor(a => a.CustomerId, _ => customerId)
			.Generate();

	private static List<Product> GetProducts(int numberToGenerate) =>
		new Faker<Product>()
			.RuleFor(p => p.Id, _ => Guid.NewGuid())
			.RuleFor(p => p.Name, (f, _) => f.Commerce.ProductName())
			.RuleFor(p => p.Description, (f, _) => f.Commerce.ProductDescription())
			.RuleFor(p => p.Price, (f, _) => decimal.Parse(f.Commerce.Price()))
			.Generate(numberToGenerate);

	private static List<Order> GetOrders(
		int numberToGenerate,
		List<Customer> customers,
		List<Product> products) =>
		new Faker<Order>()
			.RuleFor(o => o.Id, _ => Guid.NewGuid())
			.RuleFor(o => o.Customer, (f, _) => f.PickRandom(customers))
			.RuleFor(o => o.AddressId, (_, o) => o.Customer.Address.Id)
			.RuleFor(o => o.OrderItems, (f, o) => GetOrderItems(f.Random.Number(1, 12), o.Id, products))
			.RuleFor(o => o.Sum, (_, o) => o.OrderItems.Sum(oi => oi.Product.Price))
			.Generate(numberToGenerate);

	private static List<OrderItem> GetOrderItems(
		int numberToGenerate,
		Guid orderId,
		List<Product> products) =>
		new Faker<OrderItem>()
			.RuleFor(oi => oi.Id, _ => Guid.NewGuid())
			.RuleFor(oi => oi.OrderId, _ => orderId)
			.RuleFor(oi => oi.Product, (f, _) => f.PickRandom(products))
			.Generate(numberToGenerate);
}
