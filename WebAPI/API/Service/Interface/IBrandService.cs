using API.Common;
using Model.BaseEntity;

namespace API.Service.Interface
{
    public interface IBrandService : IGenericService<Brand>
    {
        RestOutput<Brand> GetAllPaging(Paging paging);
    }
}
