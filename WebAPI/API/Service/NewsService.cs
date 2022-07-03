using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<News>> Add(News entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<News>> Update(News entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<News>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
