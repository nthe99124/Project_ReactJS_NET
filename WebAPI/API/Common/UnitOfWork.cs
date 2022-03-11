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

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
        public IEnumerable<dynamic> AsDynamicEnumerable(DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => GetFromRow(row));
        }

        public dynamic GetFromRow(DataRow dr)
        {
            dynamic obj = new ExpandoObject();
            foreach (DataColumn column in dr.Table.Columns)
            {
                var dic = (IDictionary<string, object>)obj;
                dic[column.ColumnName] = dr[column];
            }
            return obj;
        }
        public IEnumerable<T> SqlQuery<T>(string query, List<SqlParameter> array = null) where T : class
        {
            return _context.Set<T>().FromSqlRaw(query, array);
        }
        /// <summary>
        /// Push SqlQuery 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="array"></param>
        /// <param name="paging">paging</param>
        /// <returns></returns>
        public async Task<DataTable> SqlQuery(string query, List<SqlParameter> array = null, Paging paging = null)
        {
            try
            {

                //chuoi nay van lay theo connection String cu
                //string connString = _context.Database.GetDbConnection().ConnectionString;
                if (paging != null) query += " ORDER BY " + paging.pagingOrderBy + " " + paging.typeSort + @" OFFSET " + (paging.pageFind - 1) * paging.pageSize + " ROWS FETCH NEXT " + paging.pageSize + " ROWS ONLY";
                string connString = @"Data Source=DESKTOP-3AIN6DL;Initial Catalog=LaptopDB;User ID=sa;Password=Nguyenthe99";
                SqlConnection conn = new SqlConnection(connString);
                //SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                using SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conn);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Parameters.AddRange(array.ToArray());

                DataTable dataTable = new DataTable();
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                conn.Close();
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
