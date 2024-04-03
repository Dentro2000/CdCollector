using cd_collection.application.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.infrastructure.DataAccessLayer;

internal static class PostgresConfigurationExtension
{
    public static IServiceCollection AddPostgres(this IServiceCollection service)
    {
        const string connectionString = "Host=localhost;Database=cd_collection;Username=sa;Password=password";
        service.AddDbContext<CDCollectionDbContext>(x => x.UseNpgsql(connectionString));

        return service;
    }
}