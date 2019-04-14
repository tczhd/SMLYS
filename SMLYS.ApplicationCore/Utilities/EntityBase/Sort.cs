using SMLYS.ApplicationCore.Interfaces.EntityBase;
using SMLYS.ApplicationCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.Utilities.EntityBase
{
    public class Sort<T> : ISortable<T> where T : class
    {
        public List<SortModel<T>> SortModels { get; }

        public Sort(params Expression<Func<T, object>>[] sortModels)
        {
            SortModels = new List<SortModel<T>>();

            foreach (var sortModel in sortModels)
            {
                Add(sortModel);
            }
        }

        public Sort<T> Add(Expression<Func<T, object>> expression, bool ascending = true)
        {
            SortModels.Add(new SortModel<T>(expression, ascending));
            return this;
        }

        public Sort<T> Add(string field, bool ascending = true)
        {
            SortModels.Add(new SortModel<T>(field, ascending));
            return this;
        }

        //public IQueryable<T> AsQueryable(IQueryable<T> query)
        //{
        //    foreach (var sortModel in SortModels)
        //    {
        //        query = string.IsNullOrWhiteSpace(sortModel.Field)
        //            ? query.OrderBy(sortModel.SortExpression, sortModel.Ascending)
        //            : query.OrderBy(sortModel.Field, sortModel.Ascending);
        //    }

        //    return query;
        //}
    }
}
