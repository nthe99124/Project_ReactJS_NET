using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(MyDbContext context) : base(context) { }
    }
}
