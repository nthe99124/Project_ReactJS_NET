using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class NewsImageService : INewsImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<NewsImage>> Add(NewsImage entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<NewsImage>> Update(NewsImage entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<NewsImage>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
