namespace CraftIQ.Inventory.Shared.Contracts.OrderDetails
{
    public record OrderDetailsOperationsContract(int Quantity,
                                                 int OrderId,
                                                 int ProductId);
}
