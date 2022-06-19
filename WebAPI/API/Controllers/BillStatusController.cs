using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillStatusController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IBillRepository _billRepository;
        private readonly IBillStatusRepository _billStatusRepository;

        public BillStatusController(IBillRepository billRepository, IBillStatusRepository billStatusRepository, UnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _billStatusRepository = billStatusRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
