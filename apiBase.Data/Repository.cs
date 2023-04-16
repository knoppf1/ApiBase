using apiBase.Core.Base;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using apiBase.Views.Enumns;
using MySql.Data.MySqlClient;
namespace apiBase.Data
{
    public class Repository<T> where T : BaseEntity
    {
        public DataContext _context;

        #region Contructor
        public Repository(DataContext context)
        {
            try
            {
                this._context = context;
            }
            catch
            {
                throw;
            }
        }

        #endregion



        #region Methods
        public async Task<T> Insert(T entity)
        {
            try
            {
                entity.Excluido = false;
                this._context.Set<T>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                this._context.Set<T>().Update(entity);
                this._context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await Task.Run(() => this._context.Set<T>().Where(filter).Where(p => p.Excluido == false).AsNoTracking());
                //return await    this._context.Set<T>().Where(filter).Where(p => p.Excluido == false).AsNoTracking();
                //return this._context.Set<T>().Where(filter).Where(p => p.Excluido == false).AsNoTracking();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> GetById(long id)
        {
            try
            {
                return this._context.Set<T>().Where(p => p.id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                return this._context.Set<T>().Where(filter).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async void Delete(long id, bool logical = true)
        {
            try
            {
                var entity = this._context.Set<T>().Where(p => p.id == id).FirstOrDefault();
                entity.Excluido = true;

                if (logical)
                    this._context.Set<T>().Update(entity);
                else
                    this._context.Set<T>().Remove(entity);
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async void DeleteWhere(Expression<Func<T, bool>> filter)
        {
            try
            {
                var entity = this._context.Set<T>().Where(filter);
                this._context.Set<T>().RemoveRange(entity);
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<decimal> GetMax(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> select)
        {
            try
            {

                return await this._context.Set<T>().Where(where).MaxAsync(select);
            }
            catch
            {
                throw;
            }
        }

        public async Task<decimal> GetMin(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> select)
        {
            try
            {
                return await this._context.Set<T>().Where(where).MinAsync(select);
            }
            catch
            {
                throw;
            }
        }
        public async Task<decimal> GetAvg(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> select)
        {
            try
            {
                var result = this._context.Set<T>().Where(where).Select(select).DefaultIfEmpty().ToList();


                return result.Average();
            }
            catch
            {
                throw;
            }
        }

        public int ExecuteSqlComand(string ComandSql)
        {

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = ComandSql;
                _context.Database.OpenConnection();
                return command.ExecuteNonQuery();
            }

        }



        #endregion
    }
}
