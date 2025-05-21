using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using System.Transactions;

namespace OrderMgt.API.Repositories
{
    public class TransactionRepository: GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
