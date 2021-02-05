using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public class ConfigurationContext : DomainBase<int>
    {
        public int ContextRef { get; set; }
        public int ApplicationRef { get; set; }
        public string Name { get; set; }

        public override int Ref => ContextRef;

        public List<ConfigurationItem> ConfigurationItems { get; set; }
    }
}
