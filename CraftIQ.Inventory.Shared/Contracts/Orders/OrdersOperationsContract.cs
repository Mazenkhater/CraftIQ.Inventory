namespace CraftIQ.Inventory.Shared.Contracts.Orders
{
    public record OrdersOperationsContract(int TotalAmount,
                                           int Status,
                                           int OrderType,
                                           DateTimeOffset expecteddeliverydate,
                                           DateTimeOffset receivedrate);
}
