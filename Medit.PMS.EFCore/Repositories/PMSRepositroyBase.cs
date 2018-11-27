using Medit.PMS.Domain;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Medit.PMS.EFCore.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class PMSRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        // 定义数据访问上下文对象
        protected readonly PMSDbContext _dbContext;

        /// <summary>
        /// 通过构造函数注入得到数据访问上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public PMSRepositoryBase(PMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(o=>_dbContext.Set<TEntity>().Remove(o));
            if (autoSave)
                Save();
        }

        public void Delete(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                Save();
        }

        public void Delete(TPrimaryKey id, bool autoSave = true)
        {
            Delete(Get(id), autoSave);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (autoSave)
                Save();
            return entity;
        }

        public TEntity InsertOrUpdate(TEntity entity, bool autoSave = true)
        {
            if (Get(entity.Id) != null)
                return Update(entity, autoSave);
            return Insert(entity, autoSave);
        }

        public IQueryable<TEntity> LoadPageList(int page, int size, out int total, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy)
        {
            var result = _dbContext.Set<TEntity>().AsQueryable();

            if (where != null)
                result = result.Where(where);
            if (orderBy != null)
                result = result.OrderBy(orderBy);
            else
                result = result.OrderBy(o => o.Id);
            total = result.Count();
            return result.Skip((page - 1) * size).Take(size);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public TEntity Update(TEntity entity, bool autoSave = true)
        {
            var obj = Get(entity.Id);
            EntityToEntity(entity, obj);
            if (autoSave)
                Save();
            return entity;

            //_dbContext.Set<TEntity>().Update(entity);
            //if (autoSave)
            //    Save();
            //return entity;
        }

        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var item in typeof(TEntity).GetProperties())
            {
                item.SetValue(pTargetObjDest, item.GetValue(pTargetObjSrc, new object[] { }), null);
            }
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParameter = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                    Expression.PropertyOrField(lambdaParameter, "Id"),
                    Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParameter);
        }
    }

    public abstract class PMSRepositoryBase<TEntity> : PMSRepositoryBase<TEntity, string> where TEntity : Entity
    {
        public PMSRepositoryBase(PMSDbContext dbContext)
            : base(dbContext) { }
    }
}
