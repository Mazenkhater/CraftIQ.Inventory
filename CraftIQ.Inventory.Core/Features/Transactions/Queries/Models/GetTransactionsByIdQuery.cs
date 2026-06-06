using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Queries.Models
{
    public class GetTransactionsByIdQuery : IRequest<Response<TransactionsContract>>
    {
        public Guid Id { get; set; }
        public GetTransactionsByIdQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}
