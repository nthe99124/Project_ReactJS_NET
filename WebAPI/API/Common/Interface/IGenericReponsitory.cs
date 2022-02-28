using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericReponsitory<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByParameter();
        Task<T> Create(T entity);
        Task<int> Update(T entity);
        T Delete(T entity);
        IEnumerable<N> SqlQuery<N>(string query, SqlParameter[] array = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();
    }
}
