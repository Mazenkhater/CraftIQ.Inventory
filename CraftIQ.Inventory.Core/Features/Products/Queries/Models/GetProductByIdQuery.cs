using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Products;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Products.Queries.Models
{
    public class GetProductByIdQuery :IRequest<Response<ProductsContract>>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
