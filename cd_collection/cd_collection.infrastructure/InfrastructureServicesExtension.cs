using cd_collection.core.Contracts;
using cd_collection.infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IItemsRepository, InMemoryItemsRepository>();
        services.AddSingleton<ICollectionRepository, InMemoryCollectionRepository>();
        services.AddTransient<ITime, Time>();

        return services;
    }
}