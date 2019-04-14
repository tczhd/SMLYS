using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.Utilities.EntityBase
{
    public class Filter<T> : AbstractFilter<T> where T : class
    {
        public Filter(params Expression<Func<T, bool>>[] filters)
        {
            foreach (var filter in filters)
            {
                Add(filter);
            }
        }

        public Filter<T> Add(Expression<Func<T, bool>> filter)
        {
            FilterList.Add(filter);
            return this;
        }

        public override IEnumerable<Expression<Func<T, bool>>> Build()
        {
            return FilterList;
        }
    }
}
