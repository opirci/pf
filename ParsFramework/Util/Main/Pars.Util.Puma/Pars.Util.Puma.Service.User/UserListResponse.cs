using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Service.User
{
    [DataContract]
    public class UserListResponse
    {
        [DataMember]
        public List<Domain.User> Users;
    }
}