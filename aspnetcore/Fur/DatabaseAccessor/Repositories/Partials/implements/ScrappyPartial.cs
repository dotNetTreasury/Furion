﻿using Fur.DatabaseAccessor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是或否</returns>
        public virtual bool IsKeySet(TEntity entity)
        {
            return EntityEntry(entity).IsKeySet;
        }

        /// <summary>
        /// 获取实体变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体变更包装对象</returns>
        public virtual EntityEntry<TEntity> EntityEntry(TEntity entity) => DbContext.Entry(entity);

        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpression">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression)
        {
            return EntityEntry(entity).Property(propertyExpression);
        }

        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(TEntity entity, string propertyName)
        {
            return EntityEntry(entity).Property(propertyName);
        }

        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyExpression">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression)
        {
            return entityEntry.Property(propertyExpression);
        }

        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyName">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName)
        {
            return entityEntry.Property(propertyName);
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns>int</returns>
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns>int</returns>
        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Attach(TEntity entity)
        {
            var entityEntry = EntityEntry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }
            return entityEntry;
        }

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            var noTrackEntites = entities.Where(u => EntityEntry(u).State == EntityState.Deleted);
            DbContext.AttachRange(noTrackEntites);
        }

        /// <summary>
        /// 获取所有的数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<DbContext> GetDbContexts()
        {
            return _dbContextPool.GetDbContexts();
        }

        /// <summary>
        /// 提交所有已更改的数据库上下文
        /// </summary>
        /// <returns>受影响行数</returns>
        public virtual int SavePoolChanges()
        {
            return _dbContextPool.SavePoolChanges();
        }

        /// <summary>
        /// 异步提交所有已更改的数据库上下文
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SavePoolChangesAsync()
        {
            return _dbContextPool.SavePoolChangesAsync();
        }
    }
}