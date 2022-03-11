
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
        Task<int> CommitAsync();
        IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new();
        DbSet<T> Set<T>() where T : class;
        IEnumerable<dynamic> AsDynamicEnumerable(DataTable table);
        dynamic GetFromRow(DataRow dr);
        IEnumerable<T> SqlQuery<T>(string query, List<SqlParameter> array = null) where T : class;
        Task<DataTable> SqlQuery(string query, List<SqlParameter> array = null, Paging paging = null);

    }
}
