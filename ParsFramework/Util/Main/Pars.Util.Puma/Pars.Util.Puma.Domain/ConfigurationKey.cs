using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public class ConfigurationKey : DomainBase<int>
    {
        public int ConfigurationKeyRef { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override int Ref => ConfigurationKeyRef;
    }
}
