namespace CraftIQ.Inventory.Shared.Contracts.Orders
{
    public record OrdersContract(int id,
                              Guid supplierid,
                              DateTimeOffset orderdate,
                              int totalamount,
                              int status,
                              DateTimeOffset expecteddeliverydate,
                              int ordertype,
                              DateTimeOffset receivedrate,
                              Guid createdBy,
                              Guid modifiedBy,
                              DateTimeOffset createdOn,
                              DateTimeOffset modifiedOn);
}
