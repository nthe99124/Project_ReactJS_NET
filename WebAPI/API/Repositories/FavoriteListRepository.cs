using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class FavoriteListRepository : GenericRepository<FavoriteList>, IFavoriteListRepository
    {
        public FavoriteListRepository(MyDbContext context) : base(context) { }
    }
}
