
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Common.Interface
{
    public interface IUnitOfWork
    {
        // don't understand why using UnitOfWork to use Function CommitAsync() but not always in the Create, Update, ... methods in GenericResponsitory
        Task CommitAsync();
        void Commit();
        DbSet<T> Set<T>() where T : class;
        Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null);
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();

    }
}