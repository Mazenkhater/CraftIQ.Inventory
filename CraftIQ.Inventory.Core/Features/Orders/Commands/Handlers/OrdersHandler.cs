using CraftIQ.Inventory.Core.Features.Orders.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.Orders.Commands.Handlers
{
    public class OrdersHandler :ResponseHandler,
                                 IRequestHandler<ADDOrderCommand,Response<OrdersContract>>,
                                 IRequestHandler<UpdateOrderCommand,Response<string>>,
                                 IRequestHandler<DeleteOrderCommand,Response<string>>
    {
        private readonly IGenericServices<OrdersOperationsContract, OrdersContract> ordersServices;

        public OrdersHandler(IGenericServices<OrdersOperationsContract, OrdersContract> ordersServices)
        {
            this.ordersServices = ordersServices;
        }
        public async Task<Response<OrdersContract>> Handle(ADDOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await ordersServices.Add(new OrdersOperationsContract(request.TotalAmount, request.Status, request.OrderType,request.Expecteddeliverydate,request.Receivedrate));
            return Created(result);
        }

        public async Task<Response<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await ordersServices.Update(request.Id,new OrdersOperationsContract(request.TotalAmount,request.Status, request.OrderType,request.Expecteddeliverydate,request.Receivedrate));
            return Success("Order updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await ordersServices.Delete(request.Id);
            return Deleted<string>("Order deleted successfully");
        }
    }
}
