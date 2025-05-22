using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class PromotionRepository: GenericRepository<Promotion>,IPromotionRepository
    {
        public PromotionRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
