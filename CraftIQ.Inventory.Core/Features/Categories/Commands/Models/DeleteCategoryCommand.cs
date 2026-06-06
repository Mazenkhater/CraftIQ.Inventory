using CraftIQ.Inventory.Core.ResponseBases;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Commands.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
