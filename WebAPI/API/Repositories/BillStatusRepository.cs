using API.Common;
using API.Repositories.Interface;
using Model.BaseEntity;

namespace API.Repositories
{
    public class BillStatusRepository : GenericRepository<BillStatus>, IBillStatusRepository
    {
        public BillStatusRepository(MyDbContext context) : base(context) { }
    }
}
