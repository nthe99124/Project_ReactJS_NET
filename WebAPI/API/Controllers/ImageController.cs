using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository, UnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
