namespace CraftIQ.Inventory.Shared.Contracts.Products
{
    public record ProductsContract(int Id,
                                 string Name,
                                 string Description,
                                 decimal UnitPrice,
                                 float Weight,
                                 float Length,
                                 float Width,
                                 float Height,
                                 decimal TaxCost,
                                 decimal ProfitPreUnit,
                                 decimal ProductionCost);
}
