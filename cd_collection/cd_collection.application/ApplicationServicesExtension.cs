using cd_collection.application.Services;
using cd_collection.application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.application;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<ICollectionsService, CollectionsService>();
        return services;
    }
}