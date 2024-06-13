using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Queries;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Queries;

internal sealed class GetCollectionQueryHandler : IQueryHandler<GetCollection, CollectionDto>
{
    private CdCollectionDbContext _dbContext;

    public GetCollectionQueryHandler(CdCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CollectionDto> HandleAsync(GetCollection query)
    {
        var collection = await _dbContext.Collections.SingleAsync(x => x.Id.Value == query.CollectionId);
        return collection.ConvertToDto();
    }
}