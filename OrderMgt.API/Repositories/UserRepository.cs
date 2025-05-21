using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class UserRepository: GenericRepository<User>,IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
