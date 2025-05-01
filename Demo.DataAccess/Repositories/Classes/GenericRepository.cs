using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return DbContext.Set<TEntity>()
                .Where(Predicate)
                .ToList();  



        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);

        }


        public void Remove(TEntity entity)
        {

            DbContext.Set<TEntity>().Remove(entity);

        }


        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);

        }

      
    }




    
}
