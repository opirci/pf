using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Domain
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        int? CreatedUserRef { get; set; }
        DateTime? ModifiedDate { get; set; }
        int? ModifiedUserRef { get; set; }
        //Guid? SessionRef { get; set; }
    }


    [DataContract]
    public class Role : DomainBase<int>, Pars.Util.Puma.Domain.IAuditable
    {
        private int _roleRef;
        public override int Ref => _roleRef;
        private string _name;
        private EntityState _entityState;
        private int? _createdUserRef;
        private DateTime? _createdDate;
        private int? _modifiedUserRef;
        private DateTime? _modifiedDate;


        [DataMember(Name = "roleRef")]
        public int RoleRef
        {
            get { return _roleRef; }
            set { this.SetProperty(ref _roleRef, value); }
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value); }
        }

        [DataMember(Name = "localeData")]
        public LocaleData LocaleData { get; set; }

        [DataMember(Name = "entityState")]
        public EntityState EntityState
        {
            get { return _entityState; }
            set { this.SetProperty(ref _entityState, value); }
        }

        [DataMember(Name = "createdUserRef")]
        public int? CreatedUserRef
        {
            get { return _createdUserRef; }
            set { this.SetProperty(ref _createdUserRef, value); }
        }

        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set { this.SetProperty(ref _createdDate, value); }
        }


        [DataMember(Name = "modifiedUserRef")]
        public int? ModifiedUserRef
        {
            get { return _modifiedUserRef; }
            set { this.SetProperty(ref _modifiedUserRef, value); }
        }


        [DataMember(Name = "modifiedDate")]
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { this.SetProperty(ref _modifiedDate, value); }
        }

        //Guid? SessionRef { get; set; }
    }
}
