using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Products.Commands.Models
{
    public class DeleteProductCommand :IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
