using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IEnumerable<T> SqlQuery<T>(string query, SqlParameter[] array = null) where T : class
        {
            return _context.Set<T>().FromSqlRaw(query, array);
        }
        public DataTable SqlQuery(string query, SqlParameter[] array = null)
        {
            var dt = new DataTable();
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                //_context.Database.CommandTimeout = 1800;


                //var conn = _context.Database.Connection;
                //var connectionState = conn.State;
                //try
                //{
                //    if (connectionState != ConnectionState.Open)
                //        conn.Open();
                //    using (var cmd = conn.CreateCommand())
                //    {
                //        cmd.CommandText = query;
                //        cmd.CommandType = CommandType.Text;
                //        if (array != null && array.Any())
                //        {
                //            cmd.Parameters.AddRange(array);
                //        }
                //        using (var reader = cmd.ExecuteReader())
                //        {
                //            dt.Load(reader);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    // error handling
                //    throw;
                //}
                //finally
                //{
                //    if (connectionState != ConnectionState.Closed)
                //        conn.Close();
                //}
                string connString = _context.Database.GetDbConnection().ToString();

                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                conn.Close();
                da.Dispose();
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
