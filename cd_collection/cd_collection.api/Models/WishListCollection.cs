namespace cd_collection.Models;

public record WishListCollection(Guid WishListId, string Name, List<Guid> CdIds, DateTime LastUpdate);
