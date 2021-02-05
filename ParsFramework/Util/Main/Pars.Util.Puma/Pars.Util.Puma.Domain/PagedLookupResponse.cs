using Pars.Core;
using System.Runtime.Serialization;

//namespace Pars.Core.Service
namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class PagedLookupResponse : LookupResponse, IPagedList
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }


    [DataContract(Name = "PagedLookupResponseG")]
    public class PagedLookupResponse<K, V> : LookupResponse<K, V>
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }

    [DataContract(Name = "PagedLookupResponseG1")]
    public class PagedLookupResponse<K, V, V1> : LookupResponse<K, V, V1>
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }

    [DataContract(Name = "PagedLookupResponseG2")]
    public class PagedLookupResponse<K, V, V1, V2> : LookupResponse<K, V, V1, V2>
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }

    [DataContract(Name = "PagedLookupResponseG3")]
    public class PagedLookupResponse<K, V, V1, V2, V3> : LookupResponse<K, V, V1, V2, V3>
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }


    [DataContract(Name = "PagedLookupResponseG4")]
    public class PagedLookupResponse<K, V, V1, V2, V3, V4> : LookupResponse<K, V, V1, V2, V3, V4>
    {
        public PagedLookupResponse() : base() { }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }
}
