using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Raptor.Data.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
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
        bool Any(object id);

    }
}
