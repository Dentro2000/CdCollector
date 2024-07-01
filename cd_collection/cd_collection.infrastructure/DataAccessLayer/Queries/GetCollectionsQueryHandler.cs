using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Queries;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Queries;

internal sealed class GetCollectionsQueryHandler : IQueryHandler<GetCollections, IEnumerable<CollectionDto>>
{
    private readonly CdCollectionDbContext _dbContext;

    public GetCollectionsQueryHandler(CdCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CollectionDto>> HandleAsync(GetCollections query) =>
        await _dbContext
            .Collections
            .Include(x => x.CdItems)
            .AsNoTracking()
            .Select(x => x.ConvertToDto())
            .ToListAsync();
}