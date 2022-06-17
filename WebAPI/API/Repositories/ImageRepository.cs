using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(MyDbContext context) : base(context) { }
    }
}
