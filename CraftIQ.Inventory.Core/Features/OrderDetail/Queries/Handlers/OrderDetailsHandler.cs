using CraftIQ.Inventory.Core.Features.OrderDetail.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.ServicesInterfaces;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Queries.Handlers
{
    public class OrderDetailsHandler :ResponseHandler,
                                        IRequestHandler<GetOrderDetailsByIdQuery,Response<OrderDetailsContract>>,
                                        IRequestHandler<GetOrderDetailsQuery,Response<PaginatedResult<List<OrderDetailsContract>>>>
    {
        private readonly IGenericServices<OrderDetailsOperationsContract, OrderDetailsContract> orderDetailsServices;

        public OrderDetailsHandler(IGenericServices<OrderDetailsOperationsContract,OrderDetailsContract> orderDetailsServices)
        {
            this.orderDetailsServices = orderDetailsServices;
        }
        public async Task<Response<OrderDetailsContract>> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await orderDetailsServices.GetById(request.Id);
            return Success(result);
        }

        public async Task<Response<PaginatedResult<List<OrderDetailsContract>>>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await orderDetailsServices.GetAll(request.PageNumber,request.PageSize,request.Search,request.OrderBy);
            return Success(result);
        }
    }
}
