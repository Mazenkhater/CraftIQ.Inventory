using CraftIQ.Inventory.Core.Features.Orders.Queries.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Orders.Queries.Handlers
{
    public class OrdersHandler :ResponseHandler,
                                 IRequestHandler<GetOrderByIDQuery,Response<OrdersContract>>,
                                 IRequestHandler<GetOrdersQuery,Response<PaginatedResult<List<OrdersContract>>>>
    {
        private readonly IGenericServices<OrdersOperationsContract, OrdersContract> ordersServices;

        public OrdersHandler(IGenericServices<OrdersOperationsContract, OrdersContract> ordersServices)
        {
            this.ordersServices = ordersServices;
        }
        public async Task<Response<OrdersContract>> Handle(GetOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await ordersServices.GetById(request.Id);
            return Success(result);
        }

        public async Task<Response<PaginatedResult<List<OrdersContract>>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await ordersServices.GetAll(request.PageNumber,request.PageSize,request.Search,request.OrderBy);
            return Success(result);
        }
    }
}
