using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericReponsitory<T>
    {
        IEnumerable<T> GetAll();
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, List<SqlParameter> array) where T : class, new();
        Task Save();

    }
}
