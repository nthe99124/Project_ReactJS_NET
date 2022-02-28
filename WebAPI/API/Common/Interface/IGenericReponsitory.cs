using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericReponsitory<T>
    {
        IEnumerable<T> GetAll();
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        IEnumerable<T> SqlQuery<T>(string query, SqlParameter[] array = null) where T : class;
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();
        Task<int> Save();

    }
}
