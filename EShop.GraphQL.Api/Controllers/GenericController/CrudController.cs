using Eshop.GraphQL.Api.Core.Endpoints;
using Eshop.GraphQL.Api.Core.Extensions;

using EShop.GraphQL.DataAccess.Models;
using EShop.GraphQL.DataAccess.Repositories;

using FluentValidation;

namespace EShop.GraphQL.Api.Controllers.GenericController;

public abstract class CrudController<TEntity, TRepo> :
	IEndpointDefinition
	where TEntity : Entity
	where TRepo : IGenericRepository<TEntity>
{
	protected abstract string EndpointName { get; }

	public virtual void DefineEndpoints(WebApplication app)
	{
		var endpoint = "/" + EndpointName;
		var endpointWithId = "/" + EndpointName + "/{id}";

		app.MapGet(endpoint, GetAll);
		app.MapGet(endpointWithId, GetById);
		app.MapPost(endpoint, Create);
		app.MapPut(endpointWithId, Update);
		app.MapDelete(endpointWithId, Delete);
	}

	internal virtual async Task<List<TEntity>> GetAll(TRepo repo) => await repo.GetAll();

	internal virtual async Task<IResult> GetById(Guid id, TRepo repo)
	{
		var entity = await repo.GetById(id, AddIncludes(repo));

		return entity is not null ? Results.Ok(entity) : Results.NotFound();
	}

	internal virtual async Task<IResult> Create(
		TRepo repo,
		TEntity entity,
		IValidator<TEntity> validator)
	{
		var validationResult = validator.Validate(entity);

		if (!validationResult.IsValid)
		{
			return Results.ValidationProblem(validationResult.ToDictionary());
		}

		await repo.Create(entity);

		return Results.Created($"/{EndpointName}/{entity.Id}", entity);
	}

	internal virtual IResult Update(
		Guid id,
		TEntity entityToUpdate,
		TRepo repo,
		IValidator<TEntity> validator)
	{
		var validationResult = validator.Validate(entityToUpdate);

		if (!validationResult.IsValid)
		{
			return Results.ValidationProblem(validationResult.ToDictionary());
		}

		var dbEntity = repo.GetById(id);

		if (dbEntity is null)
		{
			return Results.NotFound();
		}

		repo.Update(entityToUpdate);

		return Results.Ok(entityToUpdate);
	}

	internal virtual IResult Delete(Guid id, TRepo repo)
	{
		repo.Delete(id);

		return Results.Ok();
	}

	public virtual void DefineServices(IServiceCollection services)
	{
	}

	protected virtual IQueryable<TEntity>? AddIncludes(TRepo repo) =>
		Enumerable.Empty<TEntity>().AsQueryable();
}
