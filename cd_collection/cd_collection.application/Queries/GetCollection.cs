using cd_collection.application.Abstractions;
using cd_collection.application.DTO;
using cd_collection.core.Entities;

namespace cd_collection.application.Queries;

public class GetCollection : IQuery<CollectionDto>
{
    public GetCollection(Guid collectionId)
    {
        CollectionId = collectionId;
    }

    public Guid CollectionId { get; set; }
}