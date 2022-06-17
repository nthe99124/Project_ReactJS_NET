using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        int GetCountRecordAll();
        long GetLastID<TTable>(Func<TTable, dynamic> columnSelector) where TTable : class;
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(dynamic id);
        void DeleteRange(Expression<Func<T, bool>> predicate);
        Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();

    }
}
