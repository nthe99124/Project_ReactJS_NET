using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<FavoriteList>> Add(FavoriteList entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<FavoriteList>> Update(FavoriteList entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<FavoriteList>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
