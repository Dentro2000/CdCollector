using cd_collection.Repository;
using cd_collection.Services;
using cd_collection.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<ICollectionsService, CollectionsService>();
        return services;
    }
}