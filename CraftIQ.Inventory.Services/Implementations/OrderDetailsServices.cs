using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class OrderDetailsServices : IGenericServices<OrderDetailsOperationsContract, OrderDetailsContract>
    {
        private readonly IGenericRepository<OrderDetail> repository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly ICurrentUserService currentUser;

        public OrderDetailsServices(IGenericRepository<OrderDetail> repository, IGenericRepository<Product> productRepository, ICurrentUserService currentUser)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.currentUser = currentUser;
        }
        public async Task<PaginatedResult<List<OrderDetailsContract>>> GetAll(int pageNumber, int pageSize, string? search, string? orderBy)
        {
            var Query = repository.GetTableNoTracking();
            Query = orderBy?.ToLower() switch
            {
                "date" => Query.OrderBy(x => x.CreatedOn),

                _ => Query.OrderBy(x => x.OrderDetailId)
            };
            var TotalCount = await Query.CountAsync();
            var OrderDetails = await Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (TotalCount>0)
            {
                var result = OrderDetails.Select(o => new OrderDetailsContract(o.OrderDetailId,
                                                o.Quantity,
                                                o.TotalPrice,
                                                o.OrderId,
                                                o.ProductId,
                                                o.CreatedBy,
                                                o.ModifiedBy,
                                                o.CreatedOn,
                                                o.ModifiedOn)).ToList();
                return new PaginatedResult<List<OrderDetailsContract>>(result,TotalCount,pageNumber, pageSize);
            }
            else
                return new PaginatedResult<List<OrderDetailsContract>>(new List<OrderDetailsContract>(), TotalCount, pageNumber, pageSize);
        }

        public async Task<OrderDetailsContract> GetById(int id)
        {
            var OrderDetail = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.OrderDetailId == id);
            if (OrderDetail != null)
            {
                return new OrderDetailsContract(OrderDetail.OrderDetailId,
                                                OrderDetail.Quantity,
                                                OrderDetail.TotalPrice,
                                                OrderDetail.OrderId,
                                                OrderDetail.ProductId,
                                                OrderDetail.CreatedBy,
                                                OrderDetail.ModifiedBy,
                                                OrderDetail.CreatedOn,
                                                OrderDetail.ModifiedOn);
            }
            else
                throw new Exception("No OrderDetail found");
        }

        public async Task<OrderDetailsContract> Add(OrderDetailsOperationsContract contract)
        {
            var product = await productRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.ProductId == contract.ProductId);
            if (product == null)
                throw new Exception("Product Not Found");
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.Quantity = contract.Quantity;
            orderDetail.TotalPrice = contract.Quantity * product.UnitPrice;
            orderDetail.OrderId = contract.OrderId;
            orderDetail.ProductId = contract.ProductId;
            orderDetail.CreatedBy = currentUser.UserId;
            orderDetail.ModifiedBy = currentUser.UserId;
            orderDetail.CreatedOn = DateTimeOffset.UtcNow;
            orderDetail.ModifiedOn = DateTimeOffset.UtcNow;

            var result = await repository.AddAsync(orderDetail);
            if (result != null)
            {
                return new OrderDetailsContract(orderDetail.OrderDetailId,
                                             orderDetail.Quantity,
                                             orderDetail.TotalPrice,
                                             orderDetail.OrderId,
                                             orderDetail.ProductId,
                                             orderDetail.CreatedBy,
                                             orderDetail.ModifiedBy,
                                             orderDetail.CreatedOn,
                                             orderDetail.ModifiedOn);
            }
            else
                throw new Exception("Failed to add OrderDetail");
        }

        public async Task Update(int id, OrderDetailsOperationsContract contract)
        {
            var orderdetail = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.OrderDetailId == id);
            var product = await productRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.ProductId == contract.ProductId);
            if (product == null)
                throw new Exception("Product Not Found");
            if (orderdetail != null)
            {
                orderdetail.Quantity = contract.Quantity;
                orderdetail.TotalPrice = contract.Quantity * product.UnitPrice;
                orderdetail.OrderId = contract.OrderId;
                orderdetail.ProductId = contract.ProductId;
                orderdetail.CreatedBy = orderdetail.CreatedBy;
                orderdetail.CreatedOn = orderdetail.CreatedOn;
                orderdetail.ModifiedBy = currentUser.UserId;
                orderdetail.ModifiedOn = DateTimeOffset.UtcNow;
                await repository.UpdateAsync(orderdetail);
            }
            else
                throw new Exception("OrderDetail Not Found");
        }

        public async Task Delete(int id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var OrderDetail = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.OrderDetailId == id);
                if (OrderDetail != null)
                {

                    await repository.DeleteAsync(OrderDetail);
                    await trans.CommitAsync();
                }
                else
                    throw new Exception("OrderDetail Not Found");
            }
            catch
            {
                await trans.RollbackAsync();
                throw new Exception("Failed to delete OrderDetail");
            }

        }
    }
}
