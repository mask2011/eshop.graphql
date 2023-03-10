using EShop.GraphQL.DataAccess.Models;

namespace EShop.GraphQL.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : Entity
{
	IQueryable<TEntity> Query { get; }

	Task Create(TEntity entity);

	Task<TEntity> GetById(Guid id, IQueryable<TEntity> query = null);

	Task<List<TEntity>> GetAll();

	void Update(TEntity entity);

	void Delete(Guid id);
}
