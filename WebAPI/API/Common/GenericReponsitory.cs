using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Common
{
    public class GenericReponsitory<T> : IGenericReponsitory<T> where T : class
    {
        protected readonly DbSet<T> _dbset;
        protected readonly MyDbContext _context;
        protected readonly IUnitOfWork _entities;
        protected GenericReponsitory(IUnitOfWork entities)
        {
            _entities = entities;
            _dbset = _entities.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }
        public void Create(T entity)
        {
            _dbset.Add(entity);
        }
        public async Task CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
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
        public void Delete(int id)
        {
            var entity = _dbset.Find(id);
            _dbset.Remove(entity);
        }
        public async Task<DataTable> SqlQuery(string query, Paging paging = null, List<SqlParameter> array = null)
        {
            try
            {
                //array là mảng tham số truyền vào theo kiểu dữ liệu SqlParameter
                return await _entities.SqlQuery(query, paging, array);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, List<SqlParameter> array) where T : class, new()
        {
            return _entities.ExecuteStoredProcedureObject<T>(nameProcedure, array.ToArray());
        }
        public async Task Save()
        {
            await _entities.CommitAsync();
        }
    }
}
