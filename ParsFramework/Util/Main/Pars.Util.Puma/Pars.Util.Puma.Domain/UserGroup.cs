using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class UserGroup : DomainBase<int>
    {
        public override int Ref => this._userGroupRef;

        private int _userGroupRef;
        private string _name;
        //private short? _entityStateRef;
        //private string _entityStateName;

        [DataMember]
        public int UserGroupRef
        {
            get
            {
                return _userGroupRef;

            }
            set
            {
                if (_userGroupRef != value)
                    _userGroupRef = value;
            }
        }

        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                    _name = value;
            }
        }

        [DataMember]
        public EntityState EntityState { get; set; }

        [DataMember]
        public LocaleData LocaleData { get; set; }
    }
}
