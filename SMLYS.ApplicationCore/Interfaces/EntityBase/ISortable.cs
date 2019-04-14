using SMLYS.ApplicationCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.EntityBase
{
    public interface ISortable<T> where T : class
    {
        List<SortModel<T>> SortModels { get; }
    }
}
