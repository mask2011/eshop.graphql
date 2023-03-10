using EShop.GraphQL.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace EShop.GraphQL.DataAccess.Repositories;

public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity>
	where TEntity : Entity
	where TDbContext : DbContext
{
	public IQueryable<TEntity> Query => _dbContext.Set<TEntity>();

	protected readonly TDbContext _dbContext;

	public GenericRepository(TDbContext dbContext) =>
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	public Task<List<TEntity>> GetAll() => _dbContext.Set<TEntity>().ToListAsync();

	public virtual Task<TEntity> GetById(Guid id, IQueryable<TEntity> query = null)
	{
		if (query is not null)
		{
			return query.FirstOrDefaultAsync(p => p.Id == id);
		}

		return _dbContext.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);
	}

	public Task Create(TEntity entity)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		return _dbContext.Set<TEntity>().AddAsync(entity).AsTask();
	}

	public void Update(TEntity entity)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		_dbContext.Set<TEntity>().Update(entity);
	}

	public void Delete(Guid id)
	{
		var entity = _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		_dbContext.Set<TEntity>().Remove(entity);
	}
}
