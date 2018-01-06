using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Raptor.Data.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        bool Any(object id);

        IQueryable<TEntity> Include(Expression<Func<TEntity, object>> include);
        IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> IncludeMultiple(IList<Expression<Func<TEntity, object>>> includes);

        DbSet<TEntity> Table { get; }
    }
}
