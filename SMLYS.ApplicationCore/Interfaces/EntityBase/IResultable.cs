using System;
using System.Linq.Expressions;

namespace SMLYS.ApplicationCore.Interfaces.EntityBase
{
    public interface IResultable<TEntity, TResult>
       where TEntity : class, new()
       where TResult : IResultable<TEntity, TResult>, new()
    {
        Expression<Func<TEntity, TResult>> CreateResult();
    }
}
