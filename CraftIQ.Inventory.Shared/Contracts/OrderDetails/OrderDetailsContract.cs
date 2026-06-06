namespace CraftIQ.Inventory.Shared.Contracts.OrderDetails
{
    public record OrderDetailsContract(int OrderDetailId,
                                     int Quantity,
                                     decimal TotalPrice,
                                     int OrderId,
                                     int ProductId,
                                     Guid CreatedBy,
                                     Guid ModifiedBy,
                                     DateTimeOffset CreatedOn,
                                     DateTimeOffset ModifiedOn);
    
}
