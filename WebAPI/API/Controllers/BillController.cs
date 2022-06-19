using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository, UnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
