using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Common
{
    public class GenericReponsitory<T> : IGenericReponsitory<T> where T : class
    {
        protected readonly DbSet<T> _dbset;
        protected readonly MyDbContext _context;
        public GenericReponsitory(MyDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }
        public int GetCountRecordAll()
        {
            return _dbset.Count();
        }
        public long GetLastID<TTable>(Func<TTable, dynamic> columnSelector) where TTable : class
        {
            return _context.Set<TTable>().Max(columnSelector);
        }
        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            // vì đây không phải task, nên gọi Task.Run => khởi tạo Task và thực Start Task
            return await Task.Run(() => _dbset.Where(predicate).AsEnumerable<T>());
        }
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbset.FirstOrDefault(predicate));
        }
        public void Create(T entity)
        {
            _dbset.Add(entity);
        }
        public async Task CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }
        public async Task CreateRangeAsync(List<T> entity)
        {
            await _dbset.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            //_dbset.Attach(entity);
            //DbContext.Entry(entity).State = EntityState.Modified;
            //_entities.Set<Entity>();
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public void Delete(dynamic id)
        {
            var entity = _dbset.Find(id);
            _dbset.Remove(entity);
        }
        public void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbset.Where(predicate);
            _dbset.RemoveRange(entity);
        }

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
        //public virtual dynamic PushParameterToArray<TTable>(TTable entity, bool QueryString) where TTable : class
        //{
        //    var para = new List<SqlParameter>();
        //    var query = new List<string>();
        //    var properties = entity.GetType().GetProperties();
        //    //var properties = typeof(TTable).GetProperties();
        //    foreach (var item in properties)
        //    {
        //        // làm sao để phân biệt các biến kiểu Int, long khi không cho nó null, không truyền nó sẽ mặc định là 0
        //        // => làm sao phân biệt nó là cần tìm 0 hay là không cần tìm với nó???
        //        // hay phải tạo riêng 1 view model cho phép nó null
        //        // tạm thời nếu là 0 thì bỏ qua
        //        if (item?.GetValue(entity) != null || (item.PropertyType.Name == "Int64" && Convert.ToInt32(item?.GetValue(entity)) != 0))
        //        {
        //            para.Add(new SqlParameter("@" + item.Name, item?.GetValue(entity)));
        //            query.Add(" AND " + item.Name + " = @" + item.Name);
        //        }
        //    }
        //    if (QueryString)
        //    {
        //        return query;
        //    }
        //    else return para;
        //}

    }
}
