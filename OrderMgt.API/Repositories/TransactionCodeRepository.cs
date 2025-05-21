using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class TransactionCodeRepository: GenericRepository<TransactionCode>, ITransactionCodeRepository
    {
        public TransactionCodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
