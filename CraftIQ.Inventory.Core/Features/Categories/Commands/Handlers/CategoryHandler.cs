using CraftIQ.Inventory.Core.Features.Categories.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Commands.Handlers
{
    public class CategoryHandler : ResponseHandler,
                                   IRequestHandler<ADDCategoryCommand,Response<CategoriesContract>>,
                                   IRequestHandler<UpdateCategoryCommand, Response<string>>,
                                   IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        //private readonly ICategoriesServices categoriesServices;
        private readonly IGenericServices<CategoriesOperationsContract, CategoriesContract> categoriesServices;

        public CategoryHandler(IGenericServices<CategoriesOperationsContract,CategoriesContract> categoriesServices)//ICategoriesServices categoriesServices)
        {
            this.categoriesServices = categoriesServices;
        }
        public async Task<Response<CategoriesContract>> Handle(ADDCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await categoriesServices.Add(new CategoriesOperationsContract(request.Name,request.Description));
            return Created(result); //new Response<CategoriesContract> {Data= result, StatusCode = System.Net.HttpStatusCode.Created, Succeeded = true, Message = "Created" };
        }

        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoriesServices.Update(request.Id, new CategoriesOperationsContract(request.Name, request.Description));
            return Success("Category updated successfully");
        }
        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoriesServices.Delete(request.Id);
            return Deleted<string>("Category deleted successfully");
        }
    }
}
