using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        (int count, IQueryable<T> data) GetAllPaging(Paging paging);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        Task CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entity);
        void Update(T entity);
        void UpdateRange(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void DeleteById(dynamic id);
        void DeleteRange(Expression<Func<T, bool>> predicate);
        List<TTable> PagingResult<TTable>(List<TTable> lstEntity, Paging paging = null) where TTable : class;
        List<TTable> FindByAnyPoint<TTable>(List<TTable> lstEntity, TTable parameter) where TTable : class;
        // for using ADO
        Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null);
        IEnumerable<T> ExecuteStoredProcedureObject(string nameProcedure, SqlParameter[] array);

    }
}
