using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pars.Core.Data
{
    [CollectionDataContract]
    public class PagedList<T> : List<T>
    {
        [DataMember]
        public int PageNumber { get; set; }
        [DataMember]
        public int PageSize { get; set; }
        [DataMember]
        public int TotalCount { get; set; }

        public PagedList() : base() { }

        public PagedList(IEnumerable<T> items) : base(items) { }

        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize) : this(items)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PagedList(int totalCount, int pageNumber, int pageSize) 
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
