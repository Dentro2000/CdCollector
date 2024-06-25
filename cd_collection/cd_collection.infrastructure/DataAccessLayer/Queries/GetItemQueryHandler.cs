using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Queries;
using cd_collection.core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Queries;

internal class GetItemQueryHandler: IQueryHandler<GetCdItem, CdItemDto>
{
    
    private readonly CdCollectionDbContext _dbContext;


    public GetItemQueryHandler(CdCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CdItemDto> HandleAsync(GetCdItem query)
    {
        var cdItemId = new CdItemId(query.CdItemId);
        var cdItem = await _dbContext.CdItems
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == cdItemId);
        
        return cdItem.ConvertToDto();
    }
}