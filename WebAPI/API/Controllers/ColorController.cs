using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
