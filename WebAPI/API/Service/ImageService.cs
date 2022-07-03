using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<Image>> Add(Image entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Image>> Update(Image entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Image>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
