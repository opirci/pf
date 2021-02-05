using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class LocaleData : DomainBase<int>
    {
        [DataMember]
        public override int Ref => this.LocaleRef;

        [DataMember]
        public int LocaleRef { get; set; }

        private string _value;

        [DataMember]
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;

                _value = value;
                HasChanged = true;
            }
        }
    }
}
