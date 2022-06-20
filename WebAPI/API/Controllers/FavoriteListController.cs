using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;

        public FavoriteListController(IFavoriteListRepository favoriteListRepository, IUnitOfWork unitOfWork)
        {
            _favoriteListRepository = favoriteListRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
