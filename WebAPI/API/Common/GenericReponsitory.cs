using API.Common.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Common
{
    public class GenericReponsitory<T> : IGenericReponsitory<T> where T : class
    {
        protected readonly DbSet<T> _dbset;
        protected readonly MyDbContext DbContext;
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
        public async Task<T> Create(T entity)
        {
            _dbset.Add(entity);
            await Save();
            return entity;
        }
        public void Update(T entity)
        {
            _dbset.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

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
        public IEnumerable<T> SqlQuery<T>(string query, List<SqlParameter> array = null) where T : class
        {
            return _entities.SqlQuery<T>(query, array);
        }
        public async Task<DataTable> SqlQuery(string query, List<SqlParameter> array = null, Paging paging = null)
        {
            return await _entities.SqlQuery(query, array, paging);
        }
        public IEnumerable<T> ExecuteStoredProcedureObject<T>(string nameProcedure, List<SqlParameter> array) where T : class, new()
        {
            return _entities.ExecuteStoredProcedureObject<T>(nameProcedure, array.ToArray());
        }
        public async Task<int> Save()
        {
            try
            {
                return await _entities.CommitAsync();
            }
            catch (Exception ex)
            {
                return -1;
                //return _entities.Commit();
            }

        }

    }
}
