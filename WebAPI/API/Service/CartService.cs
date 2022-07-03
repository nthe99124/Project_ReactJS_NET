#nullable enable
using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<Cart>> Add(Cart entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Cart>> Update(Cart entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Cart>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
