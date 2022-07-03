using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;

namespace API.Service
{
    public class BillStatusService : IBillStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<RestOutputCommand<BillStatus>> Add(BillStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<BillStatus>> Update(BillStatus entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RestOutputCommand<BillStatus>> Delete(dynamic id)
        {
            throw new System.NotImplementedException();
        }
    }
}
