namespace CraftIQ.Inventory.Shared.Contracts.Inventories
{
    public record InventoriesContract(int Id,
                                      int Quantity,
                                      int ReorderLevel,
                                      string Location,
                                      DateTimeOffset LastUpdated,
                                      Guid CreatedBy,
                                      Guid ModifiedBy,
                                      DateTimeOffset CreatedOn,
                                      DateTimeOffset ModifiedOn);
}
