using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pars.Core.Linq
{
    public interface ITQueryable<out T> : IOrderedQueryable<T>, IQueryable, IQueryProvider, IEnumerable<T>, IEnumerable, IPagedList
    {

    }
}
