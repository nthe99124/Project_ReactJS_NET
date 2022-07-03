using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<Role>> Add(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Role>> Update(Role entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Role>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
