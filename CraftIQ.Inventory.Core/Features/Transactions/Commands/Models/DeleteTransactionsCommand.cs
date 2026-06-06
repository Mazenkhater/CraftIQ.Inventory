using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Transactions.Commands.Models
{
    public class DeleteTransactionsCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DeleteTransactionsCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}
