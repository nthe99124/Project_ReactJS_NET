using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
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
        IEnumerable<T> SqlQuery<T>(string query, List<SqlParameter> array = null) where T : class;
        Task<DataTable> SqlQuery(string query, List<SqlParameter> array = null, Paging paging = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, List<SqlParameter> array) where T : class, new();
        Task<int> Save();

    }
}
