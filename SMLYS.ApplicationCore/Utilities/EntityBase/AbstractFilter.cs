using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.Utilities.EntityBase
{
    public abstract class AbstractFilter<T> where T : class
    {
        protected List<Expression<Func<T, bool>>> FilterList { get; }

        protected AbstractFilter()
        {
            FilterList = new List<Expression<Func<T, bool>>>();
        }

        public virtual IEnumerable<Func<T, bool>> GetCompiledFilter()
        {
            foreach (var expression in FilterList)
            {
                yield return expression.Compile();
            }
        }

        protected virtual void Clear()
        {
            FilterList.Clear();
        }

        public abstract IEnumerable<Expression<Func<T, bool>>> Build();

        public static AbstractFilter<T> operator +(AbstractFilter<T> x, AbstractFilter<T> y)
        {
            var result = new Filter<T>();
            if (x != null)
                result.FilterList.AddRange(x.Build());

            if (y != null)
                result.FilterList.AddRange(y.Build());

            return result;
        }
    }
}
