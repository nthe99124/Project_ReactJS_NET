using API.Common;
using API.Service.Interface;
using Model.BaseEntity;
using System.Threading.Tasks;
using API.Common.Interface;

namespace API.Service
{
    public class BillService : IBillService
    {
        private IUnitOfWork _unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<Bill>> Add(Bill entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Bill>> Update(Bill entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<Bill>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
