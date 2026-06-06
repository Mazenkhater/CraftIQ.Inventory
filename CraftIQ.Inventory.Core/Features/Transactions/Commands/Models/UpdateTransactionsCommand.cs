using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Commands.Models
{
    public class UpdateTransactionsCommand : IRequest<Response<string>>
    {
        public Guid id { get; set; }
        public int Quantity { get; set; }
        public int TransactionType { get; set; }
        public string Notes { get; set; }
    }
}
