using LearnCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LearnCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearnCore.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class LearnCoreRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        /// <summary>
        /// 定义数据访问上下文对象
        /// </summary>
        protected readonly LearnCoreDbContext db;

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public LearnCoreRepositoryBase(LearnCoreDbContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public void Delete(TPrimaryKey id)
        {
            db.Set<TEntity>().Remove(Get(id));
        }

        /// <summary>
        /// 根据lambda表达式获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            return db.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return db.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 根据Lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式</param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            db.Set<TEntity>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity InsertOrUpdate(TEntity entity)
        {
            return Get(entity.Id) != null ? Update(entity) : Insert(entity);
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }

        /// <summary>
        /// 根据主键创建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        /// <summary>
        /// 根据Lambda表达式进行删除
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        public void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            db.Set<TEntity>().Where(where).ToList().ForEach(it => db.Set<TEntity>().Remove(it));
            if (autoSave)
            {
                Save();
            }
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="rowCount">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var result = db.Set<TEntity>().AsQueryable();

            if (where != null)
            {
                result = result.Where(where);
            }

            if (order != null)
            {
                result = result.OrderBy(order);
            }
            else
            {
                result = result.OrderBy(m => m.Id);
            }

            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }
    }

    /// <summary>
    /// 主键为Guid类型的仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class LearnCoreRepositoryBase<TEntity> : LearnCoreRepositoryBase<TEntity, Guid> where TEntity : Entity
    {
        public LearnCoreRepositoryBase(LearnCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}