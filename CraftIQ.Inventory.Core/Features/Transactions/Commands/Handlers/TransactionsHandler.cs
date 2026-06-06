using CraftIQ.Inventory.Core.Features.Transactions.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Commands.Handlers
{
    public class TransactionsHandler :ResponseHandler,
                                       IRequestHandler<ADDTransactionsCommand,Response<TransactionsContract>>,
                                       IRequestHandler<UpdateTransactionsCommand,Response<string>>,
                                       IRequestHandler<DeleteTransactionsCommand,Response<string>>
    {
        private readonly ITransactionsServices transactionsServices;

        public TransactionsHandler(ITransactionsServices transactionsServices)
        {
            this.transactionsServices = transactionsServices;
        }
        public async Task<Response<TransactionsContract>> Handle(ADDTransactionsCommand request, CancellationToken cancellationToken)
        {
            var result = await transactionsServices.AddTransaction(new TransactionsOperationsContract(request.Quantity,
                                                                                                        request.TransactionType,
                                                                                                        request.Notes));
            return Created(result);

        }

        public async Task<Response<string>> Handle(UpdateTransactionsCommand request, CancellationToken cancellationToken)
        {
            await transactionsServices.UpdateTransaction(request.id, new TransactionsOperationsContract(request.Quantity,
                                                                                                        request.TransactionType,
                                                                                                        request.Notes));
            return Success("Transaction updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteTransactionsCommand request, CancellationToken cancellationToken)
        {
            await transactionsServices.DeleteTransaction(request.Id);
            return Deleted<string>("Transaction deleted successfully");
        }
    }
}
