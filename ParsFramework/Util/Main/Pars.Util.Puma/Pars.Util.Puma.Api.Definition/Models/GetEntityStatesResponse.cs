using Pars.Util.Puma.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.Api.Definition.Models
{
    public class GetEntityStatesResponse
    {
        public List<EntityState> EntityStates { get; set; }
    }
}