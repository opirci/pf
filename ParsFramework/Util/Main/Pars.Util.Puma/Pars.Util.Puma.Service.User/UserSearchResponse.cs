using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.User
{
    [DataContract]
    public class UserSearchResponse
    {
        [DataMember]
        public List<LookupItem> Users;
    }
}