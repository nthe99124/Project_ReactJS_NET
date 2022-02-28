
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IUnitOfWork
    {
        // don't understand why using UnitOfWork to use Function CommitAsync() but not always in the Create, Update, ... methods in GenericResponsitory
        Task<int> CommitAsync();
        DbSet<T> Set<T>() where T : class;
    }
}
