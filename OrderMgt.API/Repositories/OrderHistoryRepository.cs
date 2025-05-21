using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class OrderHistoryRepository: GenericRepository<OrderHistory>, IOrderHistoryRepository
    {
        public OrderHistoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
