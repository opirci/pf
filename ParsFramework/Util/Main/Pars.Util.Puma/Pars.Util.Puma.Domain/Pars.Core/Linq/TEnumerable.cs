using System.Collections.Generic;
using System.Linq;

namespace Pars.Core.Linq
{
    public class TEnumerable<T> : EnumerableQuery<T>, ITQueryable<T>
    {
        public TEnumerable(IEnumerable<T> enumerable, IPagedList paged = null) : base(enumerable)
        {
            if (paged != null)
            {
                PageNumber = paged.PageNumber;
                PageSize = paged.PageSize;
                TotalCount = paged.TotalCount;
            }
        }

        public TEnumerable(System.Linq.Expressions.Expression expression) : base(expression)
        {
        }

        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
