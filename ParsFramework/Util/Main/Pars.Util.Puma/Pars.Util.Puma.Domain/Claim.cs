using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class Claim : DomainBase<int>
    {
        [DataMember]
        public override int Ref => this.ClaimRef;

        private int _claimRef;
        private string _name;
        
        [DataMember]
        public int ClaimRef
        {
            get { return _claimRef; }
            set { this.SetProperty(ref _claimRef, value); }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value); }
        }

        [DataMember]
        public EntityState EntityState { get; set; }

        [DataMember]
        public LocaleData LocaleData { get; set; }
    }
}
