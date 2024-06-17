using cd_collection.application.Abstractions;
using cd_collection.application.DTO;

namespace cd_collection.application.Queries;

public class GetCdItem: IQuery<CdItemDto>
{
    public Guid CdItemId;

    public GetCdItem(Guid cdItemId)
    {
        CdItemId = cdItemId;
    }
}