using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace cd_collection.infrastructure.DataAccessLayer;

internal static class PostgresConfigurationExtension
{
    public static IServiceCollection AddPostgres(this IServiceCollection service)
    {
        const string connectionString = "Host=localhost;Database=cd_collection;Port=15432;Username=sa;Password=password";
        service.AddDbContext<CdCollectionDbContext>(x => x.UseNpgsql(connectionString));

        return service;
    }
}