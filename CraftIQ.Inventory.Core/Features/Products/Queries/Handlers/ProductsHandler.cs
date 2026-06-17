using CraftIQ.Inventory.Core.Features.Products.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Products;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Products.Queries.Handlers
{
    public class ProductsHandler :ResponseHandler,
                                   IRequestHandler<GetProductByIdQuery,Response<ProductsContract>>,
                                   IRequestHandler<GetProductsQuery,Response<PaginatedResult<List<ProductsContract>>>>
    {
        private readonly IGenericServices<ProductsOperationsContract, ProductsContract> productsServices;

        public ProductsHandler(IGenericServices<ProductsOperationsContract,ProductsContract> productsServices)
        {
            this.productsServices = productsServices;
        }
        public async Task<Response<ProductsContract>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await productsServices.GetById(request.Id);
            return Success(result);
        }

        public async Task<Response<PaginatedResult<List<ProductsContract>>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await productsServices.GetAll(request.PageNumber,request.PageSize,request.Search,request.OrderBy);
            return Success(result);
        }
    }
}
