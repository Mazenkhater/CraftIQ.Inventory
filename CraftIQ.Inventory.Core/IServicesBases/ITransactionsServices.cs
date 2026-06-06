using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using MediatR;

namespace CraftIQ.Inventory.Core.IServicesBases
{
    public interface ITransactionsServices
    {
        Task<PaginatedResult<List<TransactionsContract>>> GetAllTransactions(int PageNumber,int PageSize,string? search=null, string? orderBy = null);
        Task<TransactionsContract> GetTransactionById(Guid id);
        Task<TransactionsContract> AddTransaction(TransactionsOperationsContract contract);
        Task UpdateTransaction(Guid id, TransactionsOperationsContract contract);
        Task DeleteTransaction(Guid id);
    }
}
