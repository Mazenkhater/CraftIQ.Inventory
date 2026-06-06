namespace CraftIQ.Inventory.Shared.Contracts.Inventories
{
    public record InventoriesOperationsContract(int Quantity,
                                                int ReorderLevel,
                                                string Location);

}
