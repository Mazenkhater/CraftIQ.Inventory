namespace CraftIQ.Inventory.Shared.Contracts.Transactions
{
    public record TransactionsContract(Guid TransactionId,
                                       Guid EmployeeId,
                                       DateTimeOffset TransactionDate,
                                       int Quantity,
                                       int TransactionType,
                                       string Notes,
                                       Guid CreatedBy,
                                       Guid ModifiedBy,
                                       DateTimeOffset CreatedOn,
                                       DateTimeOffset ModifiedOn);
}
