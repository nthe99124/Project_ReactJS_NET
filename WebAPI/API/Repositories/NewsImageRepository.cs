using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class NewsImageRepository : GenericRepository<NewsImage>, INewsImageRepository
    {
        public NewsImageRepository(MyDbContext context) : base(context) { }
    }
}
