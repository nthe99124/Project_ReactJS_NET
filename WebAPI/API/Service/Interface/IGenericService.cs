using API.Common;
using System.Threading.Tasks;

namespace API.Service.Interface
{
    public interface IGenericService<T> where T : class
    {
        Task<RestOutputCommand<T>> Add(T entity);
        Task<RestOutputCommand<T>> Update(dynamic id, T entity);
        Task<RestOutputCommand<T>> Delete(dynamic id);
    }
}
