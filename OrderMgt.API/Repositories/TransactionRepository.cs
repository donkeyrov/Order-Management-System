using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using System.Transactions;

namespace OrderMgt.API.Repositories
{
    public class TransactionRepository: GenericRepository<Model.Entities.Transaction> , ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
