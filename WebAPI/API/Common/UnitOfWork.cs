using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly MyDbContext _context;
        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            // sử dụng Using thì không cần Dispose
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
        /// <summary>
        /// Push SqlQuery 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <param name="paging">paging</param>
        /// <returns></returns>
        public async Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null)
        {
            try
            {
                if (paging != null) query += " ORDER BY " + paging.pagingOrderBy + " " + paging.typeSort + @" OFFSET " + (paging.pageFind - 1) * paging.pageSize + " ROWS FETCH NEXT " + paging.pageSize + " ROWS ONLY";
                string connString = _context.iConfig.GetConnectionString("Laptop");
                SqlConnection conn = new SqlConnection(connString);
                //SqlCommand cmd = new SqlCommand(query, conn);
                await conn.OpenAsync();
                // create data adapter
                using SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conn);
                da.SelectCommand.CommandType = CommandType.Text;
                if (array != null)
                {
                    da.SelectCommand.Parameters.AddRange(array.ToArray());
                }
                DataTable dataTable = new DataTable();
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                await conn.CloseAsync();
                da.Dispose();
                //var List = this.AsDynamicEnumerable(dataTable).ToList();
                return dataTable;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, SqlParameter[] array) where T : class, new()
        {
            try
            {
                //Duyệt array sqlparameter để lấy tên tạo câu query
                var sb = new StringBuilder();
                sb.Append("exec ").Append(nameProcedure);
                for (var i = 0; i < array.Length; i++)
                {
                    if (i != 0)
                    {
                        sb.Append(",").Append(array[i].ParameterName);
                    }
                    else
                    {
                        sb.Append(" ").Append(array[i].ParameterName);
                    }
                }

                nameProcedure = sb.ToString();

                //execute StoredProcedure 
                //query là câu lệnh query, 
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                var obj = _context.Set<T>().FromSqlRaw(nameProcedure, array).ToList();
                return obj;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}