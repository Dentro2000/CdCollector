using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Queries;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Queries;

internal sealed class GetCollectionQueryHandler : IQueryHandler<GetCollection, CollectionDto?>
{
    private CdCollectionDbContext _dbContext;

    public GetCollectionQueryHandler(CdCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CollectionDto?> HandleAsync(GetCollection query)
    {
        var id = new ColectionIdentfier(query.CollectionId);
        var collection = await _dbContext.Collections
            .Include(x => x.CdItems)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        return collection?.ConvertToDto();
    }
}