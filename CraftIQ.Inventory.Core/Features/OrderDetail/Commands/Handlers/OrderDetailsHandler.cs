using CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Models;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.ResponseBases;
using CraftIQ.Inventory.Core.ServicesInterfaces;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using MediatR;

namespace CraftIQ.Inventory.Core.Features.OrderDetail.Commands.Handlers
{
    public class OrderDetailsHandler :ResponseHandler,
                                        IRequestHandler<ADDOrderDetailsCommand,Response<OrderDetailsContract>>,
                                        IRequestHandler<UpdateOrderDetailsCommand,Response<string>>,
                                        IRequestHandler<DeleteOrderDetailsCommand,Response<string>>
    {
        private readonly IGenericServices<OrderDetailsOperationsContract, OrderDetailsContract> orderDetailsServices;

        public OrderDetailsHandler(IGenericServices<OrderDetailsOperationsContract, OrderDetailsContract> orderDetailsServices)
        {
            this.orderDetailsServices = orderDetailsServices;
        }
        public async Task<Response<OrderDetailsContract>> Handle(ADDOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var result = await orderDetailsServices.Add(new OrderDetailsOperationsContract(request.Quantity,
                                                                                                        request.OrderId,
                                                                                                        request.ProductId));
            return Created(result);
        }

        public async Task<Response<string>> Handle(UpdateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            await orderDetailsServices.Update(request.Id, new OrderDetailsOperationsContract(request.Quantity,
                                                                                                       request.OrderId,
                                                                                                       request.ProductId));
            return Success("Order details updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            await orderDetailsServices.Delete(request.Id);
            return Deleted<string>("Order details deleted successfully");
        }
    }
}
