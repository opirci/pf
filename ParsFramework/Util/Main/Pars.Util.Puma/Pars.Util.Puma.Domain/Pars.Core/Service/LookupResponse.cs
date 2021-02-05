using System.Collections.Generic;
using System.Runtime.Serialization;

//namespace Pars.Core.Service
namespace Pars.Util.Puma.Domain
{

    [DataContract]
    public class LookupResponse : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem>();
        }
    }

    [DataContract(Name = "LookupResponseG")]
    public class LookupResponse<K, V> : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem<K, V>> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem<K, V>>();
        }
    }

    [DataContract(Name = "LookupResponseG1")]
    public class LookupResponse<K, V, V1> : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem<K, V, V1>> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem<K, V, V1>>();
        }
    }

    [DataContract(Name = "LookupResponseG2")]
    public class LookupResponse<K, V, V1, V2> : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem<K, V, V1, V2>> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem<K, V, V1, V2>>();
        }
    }

    [DataContract(Name = "LookupResponseG3")]
    public class LookupResponse<K, V, V1, V2, V3> : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem<K, V, V1, V2, V3>> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem<K, V, V1, V2, V3>>();
        }
    }


    [DataContract(Name = "LookupResponseG4")]
    public class LookupResponse<K, V, V1, V2, V3, V4> : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<LookupItem<K, V, V1, V2, V3, V4>> Values { get; set; }

        public LookupResponse() : base()
        {
            Values = new List<LookupItem<K, V, V1, V2, V3, V4>>();
        }
    }
}