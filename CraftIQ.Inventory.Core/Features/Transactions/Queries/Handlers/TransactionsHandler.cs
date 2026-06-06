using CraftIQ.Inventory.Core.Features.Transactions.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Queries.Handlers
{
    public class TransactionsHandler :ResponseHandler,
                                        IRequestHandler<GetTransactionsByIdQuery,Response<TransactionsContract>>,
                                        IRequestHandler<GetTransactionsQuery,Response<PaginatedResult<List<TransactionsContract>>>>
    {
        private readonly ITransactionsServices transactionsServices;

        public TransactionsHandler(ITransactionsServices transactionsServices)
        {
            this.transactionsServices = transactionsServices;
        }
        public async Task<Response<TransactionsContract>> Handle(GetTransactionsByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await transactionsServices.GetTransactionById(request.Id);
            return Success(result);
        }

        public async Task<Response<PaginatedResult<List<TransactionsContract>>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var result = await transactionsServices.GetAllTransactions(request.PageNumber,request.PageSize,request.Search,request.OrderBy);
            return Success(result);
        }
    }
}
