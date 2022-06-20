using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillRepository _billRepository;
        private readonly IBillStatusRepository _billStatusRepository;

        public BillStatusController(IBillRepository billRepository, IBillStatusRepository billStatusRepository, IUnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _billStatusRepository = billStatusRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
