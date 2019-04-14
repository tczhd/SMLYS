using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.ValueObjects
{
    public class SortModel<T> where T : class
    {
        public Expression<Func<T, object>> SortExpression { get; }
        public string Field { get; }
        public bool Ascending { get; }

        public SortModel(Expression<Func<T, object>> sortExpression, bool ascending = true)
        {
            SortExpression = sortExpression;
            Ascending = ascending;
        }

        public SortModel(string field, bool ascending = true)
        {
            Field = field;
            Ascending = ascending;
        }
    }
}
