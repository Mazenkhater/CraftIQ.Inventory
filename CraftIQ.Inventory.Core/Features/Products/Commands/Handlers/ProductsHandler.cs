using CraftIQ.Inventory.Core.Features.Products.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Products;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Products.Commands.Handlers
{
    public class ProductsHandler : ResponseHandler,
                                 IRequestHandler<ADDProductCommand, Response<ProductsContract>>,
                                 IRequestHandler<UpdateProductCommand, Response<string>>,
                                 IRequestHandler<DeleteProductCommand, Response<string>>
    {
        private readonly IGenericServices<ProductsOperationsContract, ProductsContract> productsServices;

        public ProductsHandler(IGenericServices<ProductsOperationsContract, ProductsContract> productsServices)
        {
            this.productsServices = productsServices;
        }
        public async Task<Response<ProductsContract>> Handle(ADDProductCommand request, CancellationToken cancellationToken)
        {
            var result = await productsServices.Add(new ProductsOperationsContract(request.Name,
                                                                                          request.Description,
                                                                                          request.UnitPrice,
                                                                                          request.Weight,
                                                                                          request.Length,
                                                                                          request.Width,
                                                                                          request.Height,
                                                                                          request.TaxCost,
                                                                                          request.ProfitPreUnit,
                                                                                          request.ProductionCost,
                                                                                          request.CategoryId,
                                                                                          request.InventoryId));
            return Created(result);
        }

        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await productsServices.Update(request.ProductId, new ProductsOperationsContract(request.Name,
                                                                                                  request.Description,
                                                                                                  request.UnitPrice,
                                                                                                  request.Weight,
                                                                                                  request.Length,
                                                                                                  request.Width,
                                                                                                  request.Height,
                                                                                                  request.TaxCost,
                                                                                                  request.ProfitPreUnit,
                                                                                                  request.ProductionCost,
                                                                                                  request.CategoryId,
                                                                                                  request.InventoryId));
            return Success("Product updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productsServices.Delete(request.Id);
            return Deleted<string>("Product deleted successfully");
        }
    }
}
