using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<User>> Add(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<User>> Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<User>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
