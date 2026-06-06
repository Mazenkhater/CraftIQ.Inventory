using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Commands.Models
{
    public class ADDTransactionsCommand : IRequest<Response<TransactionsContract>>
    {
        public int Quantity { get; set; }
        public int TransactionType { get; set; }
        public string Notes { get; set; }
    }
}
