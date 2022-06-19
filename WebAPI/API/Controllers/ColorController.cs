using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository, UnitOfWork unitOfWork)
        {
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
