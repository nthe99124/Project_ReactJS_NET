
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
