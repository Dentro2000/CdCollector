using cd_collection.application.Abstractions;
using cd_collection.application.Commands;
using cd_collection.application.Commands.Handlers;
using cd_collection.application.Services;
using cd_collection.application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.application;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //TODO: USE BELOW:
        
        // var applicationAssembly = typeof(ICommandHandler<>).Assembly;
        //
        // services.Scan(s => s.FromAssemblies(applicationAssembly)
        //     .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime());
        //
        //
        // return services;

        
        services.AddScoped<ICommandHandler<CreateCollection>, CreateCollectionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateCollection>, UpdateCollectionCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteCollection>, DeleteCollectionCommandHandler>();
        services.AddScoped<ICommandHandler<AddItemToCollection>, AddItemToCollectionCommandHandler>();

        
        services.AddScoped<IItemsService, CdItemsService>();
        services.AddScoped<ICollectionsService, CollectionsService>();
        return services;
    }
}