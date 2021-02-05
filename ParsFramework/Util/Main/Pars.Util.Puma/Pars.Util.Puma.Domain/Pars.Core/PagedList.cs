//namespace Pars.Core.Data

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pars.Core
{

    public interface IPagedList 
    {       
        int PageNumber { get; set; }       
        int PageSize { get; set; }      
        int TotalCount { get; set; }
    }

    [DataContract]
    public class PagedList<T> : List<T>, IPagedList
    {
        [DataMember]
        public int PageNumber { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int TotalCount { get; set; }

        public PagedList() : base()
        {

        }
        public PagedList(IEnumerable<T> items) : base(items)
        {

        }
        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber = 0, int pageSize = 0) : this(items)
        {
            this.TotalCount = totalCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
