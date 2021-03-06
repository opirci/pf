﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [DataContract]
    public class ClaimListResponse
    {
        [DataMember]
        public List<Claim> Claims { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}