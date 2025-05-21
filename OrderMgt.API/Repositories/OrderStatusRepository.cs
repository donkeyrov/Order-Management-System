using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class OrderStatusRepository: GenericRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(AppDbContext context) : base(context)
        {
        }
    }
}
