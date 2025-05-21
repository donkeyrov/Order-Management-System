using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class SegmentRepository: GenericRepository<Segment>, ISegmentRepository
    {
        public SegmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
