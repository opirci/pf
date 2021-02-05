using System.Runtime.Serialization;

namespace Pars.Core.Linq
{
    [DataContract]
    public class Paging : IPaging
    {
        [DataMember(Name ="pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }

        public Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public Paging()
        {

        }
    }
}
