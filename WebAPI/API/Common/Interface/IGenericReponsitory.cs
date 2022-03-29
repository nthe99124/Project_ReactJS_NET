using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericReponsitory<T> where T : class
    {
        IEnumerable<T> GetAll();
        long GetLastID<TTable>(Func<TTable, dynamic> columnSelector) where TTable : class;
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();

    }
}
