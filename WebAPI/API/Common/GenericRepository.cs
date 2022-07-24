using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Common
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        protected readonly MyDbContext _context;

        protected GenericRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            var result = _dbSet.AsEnumerable<T>();
            return result;
        }

        public (long totalRecord, IQueryable<T> data) GetAllDataPaging(Paging paging)
        {
            var result = _dbSet.AsQueryable();
            var totalRecord = Convert.ToInt64(result.Count());
            if (paging != null)
            {
                result = result.Skip((paging.PageFind - 1) * paging.PageSize)
                    .Take(paging.PageSize);
            }
            return (totalRecord, result);
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate).AsEnumerable<T>());
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.FirstOrDefault(predicate));
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.Any(predicate));
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            //Dbset.Attach(lstEntity);
            //DbContext.Entry(lstEntity).State = EntityState.Modified;
            //_entities.Set<Entity>();
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(Expression<Func<T, bool>> predicate)
        {
            //Dbset.Attach(lstEntity);
            //DbContext.Entry(lstEntity).State = EntityState.Modified;
            //_entities.Set<Entity>();
            var entity = _dbSet.Where(predicate);
            _dbSet.UpdateRange(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteById(dynamic id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbSet.Where(predicate);
            _dbSet.RemoveRange(entity);
        }

        public void DeleteRange(IEnumerable<T> lstEntity)
        {
            _dbSet.RemoveRange(lstEntity);
        }

        public List<TTable> PagingResult<TTable>(List<TTable> lstEntity, Paging paging = null) where TTable : class
        {
            try
            {
                if (paging != null)
                {
                    return lstEntity.Skip((paging.PageFind - 1) * paging.PageSize)
                                    .Take(paging.PageSize)
                                    .ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<TTable> FindByAnyPoint<TTable>(List<TTable> lstEntity, TTable parameter) where TTable : class
        {
            var properties = parameter.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item?.GetValue(parameter) != null)
                {
                    lstEntity = lstEntity.Where(e => e.GetType().GetProperty(item.Name)!.GetValue(e)!.Equals(item?.GetValue(parameter))).ToList();
                }
            }
            return lstEntity;
        }

        // for using ADO
        public async Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null)
        {
            try
            {
                //if (paging != null) query += " ORDER BY " + paging.PagingOrderBy + " " + paging.TypeSort + @" OFFSET " + (paging.PageFind - 1) * paging.PageSize + " ROWS FETCH NEXT " + paging.PageSize + " ROWS ONLY";
                if (paging != null) query += @" OFFSET " + (paging.PageFind - 1) * paging.PageSize + " ROWS FETCH NEXT " + paging.PageSize + " ROWS ONLY";
                string connString = _context.Config.GetConnectionString("Laptop");
                var conn = new SqlConnection(connString);
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
                var dataTable = new DataTable();
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

        public IEnumerable<T> ExecuteStoredProcedureObject(string nameProcedure, SqlParameter[] array)
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
