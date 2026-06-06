using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class TransactionsServices : ITransactionsServices
    {
        private readonly IGenericRepository<Transaction> repository;
        private readonly ICurrentUserService currentUser;

        public TransactionsServices(IGenericRepository<Transaction> repository, ICurrentUserService currentUser)
        {
            this.repository = repository;
            this.currentUser = currentUser;
        }
        public async Task<TransactionsContract> GetTransactionById(Guid id)
        {
            var transaction = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transaction != null)
            {
                return new TransactionsContract(transaction.TransactionId,
                                            transaction.EmployeeId,
                                            transaction.TransactionDate,
                                            transaction.Quantity,
                                            transaction.TransactionType,
                                            transaction.Notes,
                                            transaction.CreatedBy,
                                            transaction.ModifiedBy,
                                            transaction.CreatedOn,
                                            transaction.ModifiedOn);
            }
            else
                throw new Exception("No Transaction found");
        }
       public async Task<PaginatedResult<List<TransactionsContract>>> GetAllTransactions(int PageNumber, int PageSize, string? search, string? orderBy)
        {
            var Query = repository.GetTableNoTracking();
            Query = orderBy?.ToLower() switch
            {
                "transactiondate" => Query.OrderBy(x => x.TransactionDate),


                _ => Query.OrderBy(x => x.TransactionId)
            };
            var TotalCount = await Query.CountAsync();
            var transactions = await Query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            if (transactions.Any())
            {
                var result = transactions.Select(T => new TransactionsContract(T.TransactionId,
                                                                               T.EmployeeId,
                                                                               T.TransactionDate,
                                                                               T.Quantity,
                                                                               T.TransactionType,
                                                                               T.Notes,
                                                                               T.CreatedBy,
                                                                               T.ModifiedBy,
                                                                               T.CreatedOn,
                                                                               T.ModifiedOn
                                                                               )).ToList();
                return new PaginatedResult<List<TransactionsContract>>(result, TotalCount, PageNumber, PageSize);
            }
            else
                return new PaginatedResult<List<TransactionsContract>>(new List<TransactionsContract>(), TotalCount, PageNumber,PageSize);
        }
        public async Task<TransactionsContract> AddTransaction(TransactionsOperationsContract contract)
        {
            var transaction = new Transaction();
            transaction.TransactionId = Guid.NewGuid();
            transaction.EmployeeId = Guid.NewGuid();
            transaction.TransactionDate = DateTimeOffset.UtcNow;
            transaction.Quantity = contract.Quantity;
            transaction.TransactionType = contract.TransactionType;
            transaction.Notes = contract.Notes;
            transaction.CreatedBy = currentUser.UserId;
            transaction.ModifiedBy = currentUser.UserId;
            transaction.CreatedOn = DateTimeOffset.UtcNow;
            transaction.ModifiedOn = DateTimeOffset.UtcNow;
            var result = await repository.AddAsync(transaction);
            if (result != null)
            {
                return new TransactionsContract(result.TransactionId,
                                            result.EmployeeId,
                                            result.TransactionDate,
                                            result.Quantity,
                                            result.TransactionType,
                                            result.Notes,
                                            result.CreatedBy,
                                            result.ModifiedBy,
                                            result.CreatedOn,
                                            result.ModifiedOn);
            }
            else
                throw new Exception("Failed to add Transaction");
        }
        public async Task UpdateTransaction(Guid id, TransactionsOperationsContract contract)
        {
            var transaction = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transaction != null)
            {
                transaction.Quantity = contract.Quantity;
                transaction.TransactionType = contract.TransactionType;
                transaction.Notes = contract.Notes;
                transaction.ModifiedBy = currentUser.UserId;
                transaction.ModifiedOn = DateTimeOffset.UtcNow;
                transaction.EmployeeId = currentUser.UserId;
                transaction.TransactionDate = transaction.TransactionDate;
                transaction.CreatedOn = transaction.CreatedOn;
                transaction.CreatedBy = transaction.CreatedBy;
                await repository.UpdateAsync(transaction);
            }
            else
                throw new Exception("Transaction Not Found");
        }

        public async Task DeleteTransaction(Guid id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var transaction = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.TransactionId == id);
                if (transaction != null)
                {
                    await repository.DeleteAsync(transaction);
                    await trans.CommitAsync();
                }
                else
                    throw new Exception("Transaction Not Found");
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception($"Failed to delete Transaction: {ex.Message}");
            }
        }
    }
}
