using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public class ConfigurationItem : DomainBase<int>
    {
        public int ConfigurationItemRef { get; set; }
        public int ContextRef { get; set; }
        public int ConfigurationKeyRef { get; set; }
        public string Value { get; set; }
        public override int Ref => ConfigurationItemRef;
        public string ConfigurationKeyName { get; set; }
    }
}
