using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Queries;
using cd_collection.core.Contracts;
using cd_collection.infrastructure.DataAccessLayer;
using cd_collection.infrastructure.DataAccessLayer.Queries;
using cd_collection.infrastructure.DataAccessLayer.Repositories;
using cd_collection.infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddScoped<IItemsRepository, DbItemsRepository>();
        services.AddScoped<ICollectionRepository, DbCollectionRepository>();
        services.AddScoped<IQueryHandler<GetCollections, IEnumerable<CollectionDto>>, GetCollectionsQueryHandler>();  
        services.AddScoped<IQueryHandler<GetCollection, CollectionDto>, GetCollectionQueryHandler>();  
        services.AddTransient<ITime, Time>();
       

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}