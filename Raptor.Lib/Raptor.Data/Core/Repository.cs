using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Raptor.Data.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected RaptorDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(RaptorDbContext context) {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public bool Any(object id) {
            return DbSet.Find(id) != null;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") {
            IQueryable<TEntity> query = DbSet;

            if (filter != null) {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public virtual TEntity GetById(object id) {
            return DbSet.Find(id);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return DbSet.SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll() {
            return DbSet.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return DbSet.Where(predicate);
        }

        public void Create(TEntity entity) {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void CreateRange(IEnumerable<TEntity> entities) {
            DbSet.AddRange(entities);
            Context.SaveChanges();
        }

        public void Update(TEntity entity) {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(TEntity entityToDelete) {
            if (Context.Entry(entityToDelete).State == EntityState.Detached) {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<TEntity> entities) {
            DbSet.RemoveRange(entities);
            Context.SaveChanges();
        }

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> include) {
            return DbSet.Include(include);
        }

        public IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includes) {
            IQueryable<TEntity> query = null;

            foreach (var include in includes) {
                query = DbSet.Include(include);
            }

            return query == null ? DbSet : query;
        }

        public IQueryable<TEntity> IncludeMultiple(IList<Expression<Func<TEntity, object>>> includes) {
            IQueryable<TEntity> query = null;

            foreach (var include in includes) {
                query = DbSet.Include(include);
            }

            return query == null ? DbSet : query;
        }
    }
}
