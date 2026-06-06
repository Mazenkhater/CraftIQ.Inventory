using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.IRepositoryBases;
using CraftIQ.Inventory.Core.IServicesBases;
using CraftIQ.Inventory.Core.Weappers;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using Microsoft.EntityFrameworkCore;

namespace CraftIQ.Inventory.Services.Implementations
{
    public class OrdersServices : IGenericServices<OrdersOperationsContract, OrdersContract>
    {
        private readonly IGenericRepository<Order> repository;
        private readonly ICurrentUserService currentUser;

        public OrdersServices(IGenericRepository<Order> repository, ICurrentUserService currentUser)
        {
            this.repository = repository;
            this.currentUser = currentUser;
        }
        public async Task<PaginatedResult<List<OrdersContract>>> GetAll(int pageNumber, int pageSize, string? search, string? orderBy)
        {
            var Query = repository.GetTableNoTracking();
            if (!string.IsNullOrEmpty(search))    //search!="" && search!="null") 
            {
                search =search.ToLower();

                Query = Query.Where(c =>
                    c.OrderId.CompareTo(int.Parse(search))==0
                );
            }
            Query = orderBy?.ToLower() switch
            {
                "orderdate" => Query.OrderBy(x => x.OrderDate),
                "supplierid" => Query.OrderBy(x => x.SupplierId),
                "date" => Query.OrderBy(x => x.CreatedOn),


                _ => Query.OrderBy(x => x.OrderId)
            };
            var TotalCount = await Query.CountAsync();
            var orders = await Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (orders.Any())
            {
                var result = orders.Select(o => new OrdersContract
                (
                     o.OrderId,
                     o.SupplierId,
                     o.OrderDate,
                     o.TotalAmount,
                     o.Status,
                     o.ExpectedDeliveryDate,
                     o.OrderType,
                     o.ReceivedDate,
                     o.CreatedBy,
                     o.ModifiedBy,
                     o.CreatedOn,
                     o.ModifiedOn
                )).ToList();
                return new PaginatedResult<List<OrdersContract>>(result,TotalCount,pageNumber, pageSize);
            }
            else
                return new PaginatedResult<List<OrdersContract>>(new List<OrdersContract>(), TotalCount, pageNumber, pageSize);
        }

        public async Task<OrdersContract> GetById(int id)
        {
            var Order = await repository.GetTableNoTracking().FirstOrDefaultAsync(x => x.OrderId == id);
            if (Order != null)
            {
                return new OrdersContract(Order.OrderId,
                                               Order.SupplierId,
                                               Order.OrderDate,
                                               Order.TotalAmount,
                                               Order.Status,
                                               Order.ExpectedDeliveryDate,
                                               Order.OrderType,
                                               Order.ReceivedDate,
                                               Order.CreatedBy,
                                               Order.ModifiedBy,
                                               Order.CreatedOn,
                                               Order.ModifiedOn);
            }
            else
                throw new Exception("No Order found");
        }

        public async Task<OrdersContract> Add(OrdersOperationsContract contract)
        {
            var order = new Order();
            order.SupplierId = Guid.NewGuid();
            order.OrderDate = DateTimeOffset.UtcNow;
            order.TotalAmount = contract.TotalAmount;
            order.Status = contract.Status;
            order.ExpectedDeliveryDate = contract.expecteddeliverydate;
            order.OrderType = contract.OrderType;
            order.ReceivedDate = contract.receivedrate;
            order.CreatedBy = currentUser.UserId;
            order.ModifiedBy = currentUser.UserId;
            order.CreatedOn = DateTimeOffset.UtcNow;
            order.ModifiedOn = DateTimeOffset.UtcNow;
            var Order = await repository.AddAsync(order);
            if (Order != null)
            {
                return new OrdersContract(Order.OrderId,
                                               Order.SupplierId,
                                               Order.OrderDate,
                                               Order.TotalAmount,
                                               Order.Status,
                                               Order.ExpectedDeliveryDate,
                                               Order.OrderType,
                                               Order.ReceivedDate,
                                               Order.CreatedBy,
                                               Order.ModifiedBy,
                                               Order.CreatedOn,
                                               Order.ModifiedOn);
            }
            else
                throw new Exception("Failed to add Order");
        }

        public async Task Update(int id, OrdersOperationsContract contract)
        {
            var order = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                order.TotalAmount = contract.TotalAmount;
                order.Status = contract.Status;
                order.OrderType = contract.OrderType;
                order.ModifiedBy = currentUser.UserId;
                order.ModifiedOn = DateTimeOffset.UtcNow;
                order.ExpectedDeliveryDate = contract.expecteddeliverydate;
                order.ReceivedDate = contract.receivedrate;
                order.SupplierId = currentUser.UserId;// مينفعش اغيرة عشان كدا كل هربطه بجدول بمورد جديد
                order.OrderDate = order.OrderDate;// مينفعش ياعدل عشان دا تاريخ الطلب
                order.CreatedOn = order.CreatedOn;
                order.CreatedBy = order.CreatedBy;
                await repository.UpdateAsync(order);
            }
            else
                throw new Exception("Order Not Found");
        }

        public async Task Delete(int id)
        {
            var trans = repository.BeginTransaction();
            try
            {
                var order = await repository.GetTableAsTracking().FirstOrDefaultAsync(x => x.OrderId == id);
                if (order != null)
                {
                    await repository.DeleteAsync(order);
                    await trans.CommitAsync();
                }
                else
                    throw new Exception("Order Not Found");
            }
            catch
            {
                await trans.RollbackAsync();
                throw new Exception("Failed to delete Order");
            }

        }
    }
}
