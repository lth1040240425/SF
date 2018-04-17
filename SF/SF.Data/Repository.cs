using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Data;
using SqlSugar;

namespace SF.Data
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        public Repository(SFContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public SFContext dbContext { get; }

        public TEntity GetById(TKey id)
        {
            return dbContext.Queryable<TEntity>().InSingle(id);
        }
        public List<TEntity> GetList()
        {
            return dbContext.Queryable<TEntity>().ToList();
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return dbContext.Queryable<TEntity>().Where(whereExpression).ToList();
        }
        public List<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page)
        {
            int count = 0;
            var result = dbContext.Queryable<TEntity>().Where(whereExpression).ToPageList(page.PageIndex, page.PageSize, ref count);
            page.PageCount = count;
            return result;
        }
        public List<TEntity> GetPageList(List<IConditionalModel> conditionalList, PageModel page)
        {
            int count = 0;
            var result = dbContext.Queryable<TEntity>().Where(conditionalList).ToPageList(page.PageIndex, page.PageSize, ref count);
            page.PageCount = count;
            return result;
        }
        public bool IsAny(Expression<Func<TEntity, bool>> whereExpression)
        {
            return dbContext.Queryable<TEntity>().Where(whereExpression).Any();
        }
        public bool Insert(TEntity insertObj)
        {
            return this.dbContext.Insertable(insertObj).ExecuteCommand() > 0;
        }
        public int InsertReturnIdentity(TEntity insertObj)
        {
            return this.dbContext.Insertable(insertObj).ExecuteReturnIdentity();
        }
        public bool InsertRange(TEntity[] insertObjs)
        {
            return this.dbContext.Insertable(insertObjs).ExecuteCommand() > 0;
        }
        public bool InsertRange(List<TEntity>[] insertObjs)
        {
            return this.dbContext.Insertable(insertObjs).ExecuteCommand() > 0;
        }
        public bool Update(TEntity updateObj)
        {
            return this.dbContext.Updateable(updateObj).ExecuteCommand() > 0;
        }
        public bool Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression)
        {
            return this.dbContext.Updateable<TEntity>().UpdateColumns(columns).Where(whereExpression).ExecuteCommand() > 0;
        }
        public bool Delete(TEntity deleteObj)
        {
            return this.dbContext.Deleteable<TEntity>().Where(deleteObj).ExecuteCommand() > 0;
        }
        public bool Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return this.dbContext.Deleteable<TEntity>().Where(whereExpression).ExecuteCommand() > 0;
        }
        public bool DeleteById(TKey id)
        {
            return this.dbContext.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
        }
        public bool DeleteByIds(TKey[] ids)
        {
            return this.dbContext.Deleteable<TEntity>().In(ids).ExecuteCommand() > 0;
        }
    }
}
