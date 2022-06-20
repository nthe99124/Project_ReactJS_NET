using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository, IUnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
