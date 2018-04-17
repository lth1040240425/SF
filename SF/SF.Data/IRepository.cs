using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SF.Core.Data;
using SF.Core.Dependency;
using SqlSugar;

namespace SF.Data
{
    public interface IRepository<TEntity, TKey> : IScopeDependency where TEntity : class, IEntity<TKey>, new()
    {
        SFContext dbContext { get; }
        TEntity GetById(TKey id);
        List<TEntity> GetList();
        List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression);
        List<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page);
        List<TEntity> GetPageList(List<IConditionalModel> conditionalList, PageModel page);
        bool IsAny(Expression<Func<TEntity, bool>> whereExpression);
        bool Insert(TEntity insertObj);
        int InsertReturnIdentity(TEntity insertObj);
        bool InsertRange(TEntity[] insertObjs);
        bool InsertRange(List<TEntity>[] insertObjs);
        bool Update(TEntity updateObj);
        bool Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);
        bool Delete(TEntity deleteObj);
        bool Delete(Expression<Func<TEntity, bool>> whereExpression);
        bool DeleteById(TKey id);
        bool DeleteByIds(TKey[] ids);
    }
}
