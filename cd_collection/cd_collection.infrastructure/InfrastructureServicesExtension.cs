using cd_collection.core.Contracts;
using cd_collection.infrastructure.DataAccessLayer;
using cd_collection.infrastructure.DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres();
        services.AddScoped<IItemsRepository, DbItemsRepository>();
        services.AddScoped<ICollectionRepository, DbCollectionRepository>();
        services.AddTransient<ITime, Time>();
       

        return services;
    }
}