using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context) { }
    }
}
