using cd_collection.application.Abstractions;
using cd_collection.application.DTO;

namespace cd_collection.application.Queries;

public class GetItem: IQuery<CdItemDto>
{
    public Guid CdItemId;

    public GetItem(Guid cdItemId)
    {
        CdItemId = cdItemId;
    }
}