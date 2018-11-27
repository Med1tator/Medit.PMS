using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Medit.PMS.Domain.IRepositories
{
    public interface IRepository
    { }

    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        List<TEntity> GetAll();

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TPrimaryKey id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity, bool autoSave = true);

        TEntity Update(TEntity entity, bool autoSave = true);

        TEntity InsertOrUpdate(TEntity entity, bool autoSave = true);

        void Delete(TEntity entity, bool autoSave = true);

        void Delete(TPrimaryKey id, bool autoSave = true);

        void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);

        IQueryable<TEntity> LoadPageList(int page, int size, out int total, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy);

        void Save();
    }

    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : Entity
    { }
}
