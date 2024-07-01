using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.application.Extensions;
using cd_collection.application.Queries;
using Microsoft.EntityFrameworkCore;

namespace cd_collection.infrastructure.DataAccessLayer.Queries;

internal class GetItemsQueryHandler: IQueryHandler<GetItems, IEnumerable<CdItemDto>>
{
    private readonly CdCollectionDbContext _dbContext;

    public GetItemsQueryHandler(CdCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<CdItemDto>> HandleAsync(GetItems query) => await
        _dbContext.CdItems
            .AsNoTracking()
            .Select(x => x.ConvertToDto())
            .ToListAsync();

}