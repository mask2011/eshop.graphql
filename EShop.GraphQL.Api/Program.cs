using Eshop.GraphQL.Api;
using Eshop.GraphQL.Api.Core.Endpoints;

using EShop.GraphQL.DataAccess;
using EShop.GraphQL.DataAccess.Schema.Mutations;
using EShop.GraphQL.DataAccess.Schema.Queries;

using FluentValidation;


using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=app.db;Cache=Shared"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints(typeof(IAssemblyMarker));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>(
    lifetime: ServiceLifetime.Scoped);

//GraphQL
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<OrderQuery>()
    .AddTypeExtension<ProductQuery>()
    .AddTypeExtension<OrderMutation>()
    .AddTypeExtension<ProductMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services?.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<AppDbContext>();
    context?.Database.Migrate();

    if (context is not null && !context.Customer.Any())
    {
        DataSeed.Seed(context);
    }
}

app.UseHttpsRedirection();

app.UseEndpoints();

app.MapGraphQL();

app.Run();
