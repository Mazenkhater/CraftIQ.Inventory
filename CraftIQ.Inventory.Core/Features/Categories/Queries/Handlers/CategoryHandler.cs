using CraftIQ.Inventory.Core.Features.Categories.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Categories.Queries.Handlers
{
    public class CategoryHandler :ResponseHandler,
                                   IRequestHandler<GetCategoryByIDQuery,Response<CategoriesContract>>,
                                   IRequestHandler<GetCategoriesQuery,Response<PaginatedResult<List<CategoriesContract>>>>
    {
        //private readonly ICategoriesServices categoriesServices;
        private readonly IGenericServices<CategoriesOperationsContract, CategoriesContract> categoriesServices;

        public CategoryHandler(IGenericServices<CategoriesOperationsContract,CategoriesContract> categoriesServices)//ICategoriesServices categoriesServices)
        {
            this.categoriesServices = categoriesServices;
        }
        public async Task<Response<CategoriesContract>> Handle(GetCategoryByIDQuery request, CancellationToken cancellationToken)
        {
            var student = await categoriesServices.GetById(request.Id);
            return Success(student);
        }

        public async Task<Response<PaginatedResult<List<CategoriesContract>>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var students = await categoriesServices.GetAll(request.PageNumber,request.PageSize,request.Search,request.OrderBy);
            return Success(students);
        }
    }
}
