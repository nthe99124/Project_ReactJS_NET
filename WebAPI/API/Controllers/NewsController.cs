using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository, IUnitOfWork unitOfWork)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
