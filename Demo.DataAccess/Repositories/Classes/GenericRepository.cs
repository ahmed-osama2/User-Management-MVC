using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.models.DepartmentModel;
using Demo.DataAccess.models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext DbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public TEntity? GetById(int id) => DbContext.Set<TEntity>().Find(keyValues: id);


        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return DbContext.Set<TEntity>().Where( E => E.IsDeleted != true).ToList();
            else
                return DbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        //use  I IQueryable

        public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector)
        {
            return DbContext.Set<TEntity>().Where(E => E.IsDeleted != true).Select(selector).ToList();


        }

        public int Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            return DbContext.SaveChanges();

        }


        public int Remove(TEntity entity)
        {

            DbContext.Set<TEntity>().Remove(entity);
            return DbContext.SaveChanges();

        }


        public int Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            return DbContext.SaveChanges();

        }

       
    }





}
