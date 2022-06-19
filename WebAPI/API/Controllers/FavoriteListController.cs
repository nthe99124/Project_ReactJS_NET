using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;

        public FavoriteListController(IFavoriteListRepository favoriteListRepository, UnitOfWork unitOfWork)
        {
            _favoriteListRepository = favoriteListRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
