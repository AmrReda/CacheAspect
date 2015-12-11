using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions; 
using CacheAspect.Model; 

namespace CacheAspect.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly StoreDataContext Context;
        public readonly DbSet<TEntity> DbSet;

        public GenericRepository(StoreDataContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        [Cache(CacheAction.Add)]
        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        [Cache(CacheAction.Add)]
        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DbSet;

            return query.ToList();
        }

        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        [Cache(CacheAction.Remove)]
        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        [Cache(CacheAction.Remove)]
        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);

            Context.Entry(entity).State = EntityState.Modified;
        }

        [Cache(CacheAction.Remove)]
        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }
    }
}
