using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<Color>> Add(Color entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Color>> Update(Color entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Color>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
