using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;

namespace SMLYS.ApplicationCore.Extensions
{
    public static class QueryableExtension
    {
        public static List<T> ToList<T>(this IQueryable<T> query, bool useNoLock) where T : class, new()
        {
            if (!useNoLock)
                return query.ToList();

            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            List<T> result;
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                result = query.ToList();
                transactionScope.Complete();
            }
            return result;
        }

        public static T FirstOrDefault<T>(this IQueryable<T> query, bool noLock) where T : class, new()
        {
            if (!noLock)
                return query.FirstOrDefault();

            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            T result;
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                result = query.FirstOrDefault();
                transactionScope.Complete();
            }
            return result;
        }

        public static int Count<T>(this IQueryable<T> query, bool noLock) where T : class, new()
        {
            if (!noLock)
                return query.Count();

            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                var result = query.Count();
                transactionScope.Complete();
                return result;
            }
        }

        public static bool Any<T>(this IQueryable<T> query, bool noLock) where T : class, new()
        {
            if (!noLock)
                return query.Any();

            var transactionOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                var result = query.Any();
                transactionScope.Complete();
                return result;
            }
        }

        public static IQueryable<T> PagedList<T>(this IQueryable<T> query, int page, int offset) where T : class, new()
        {
            return query.Skip(page * offset)
                .Take(offset);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Expression<Func<T, object>> keySelector, bool ascending)
        {
            var selectorBody = keySelector.Body;
            if (selectorBody.NodeType == ExpressionType.Convert)
                selectorBody = ((UnaryExpression)selectorBody).Operand;

            var selector = Expression.Lambda(selectorBody, keySelector.Parameters);
            MethodCallExpression queryBody;
            if (source.Expression.Type != typeof(IOrderedQueryable<T>))
                queryBody = Expression.Call(typeof(Queryable), ascending ? "OrderBy" : "OrderByDescending",
                    new[] { typeof(T), selectorBody.Type }, source.Expression, Expression.Quote(selector));
            else
                queryBody = Expression.Call(typeof(Queryable), ascending ? "ThenBy" : "ThenByDescending",
                    new[] { typeof(T), selectorBody.Type }, source.Expression, Expression.Quote(selector));
            return source.Provider.CreateQuery<T>(queryBody);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string field, bool ascending)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            PropertyInfo property;
            Expression propertyAccess;
            if (field.Contains('.'))
            {
                var childProperties = field.Split('.');
                property = type.GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (var i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(T).GetProperty(field);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            var lambdaExpression = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression orderExpression;
            if (source.Expression.Type != typeof(IOrderedQueryable<T>))
                orderExpression = Expression.Call(typeof(Queryable), @ascending ? "OrderBy" : "OrderByDescending",
                    new[] { type, property.PropertyType }, source.Expression, Expression.Quote(lambdaExpression));
            else
                orderExpression = Expression.Call(typeof(Queryable), @ascending ? "ThenBy" : "ThenByDescending",
                    new[] { type, property.PropertyType }, source.Expression, Expression.Quote(lambdaExpression));

            return source.Provider.CreateQuery<T>(orderExpression);
        }
    }
}
