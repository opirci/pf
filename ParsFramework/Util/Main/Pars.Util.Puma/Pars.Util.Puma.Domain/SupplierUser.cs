using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class SupplierUser : DomainBase<int>
    {
        public override int Ref => this.UserRef;

        [DataMember(Name = "userRef")]
        public int UserRef { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "partyRef")]
        public int PartyRef { get; set; }

        [DataMember(Name="supplierCode")]
        public string SupplierCode { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }

        [DataMember(Name = "isAdmin")]
        public bool IsAdmin { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "claims")]
        public List<UserClaim> Claims { get; set; }

        [DataMember(Name = "userLogonRef")]
        public int UserLogonRef { get; set; }

        [DataMember(Name = "userMailRef")]
        public Nullable<int> UserMailRef { get; set; }

        [DataMember(Name = "userUid")]
        public System.Guid UserUid { get; set; }

        [DataMember(Name = "createdUserRef")]
        public int CreatedUserRef { get; set; }

        [DataMember(Name = "createdDate")]
        public System.DateTime CreatedDate { get; set; }

        [DataMember(Name = "modifiedUserRef")]
        public int ModifiedUserRef { get; set; }

        [DataMember(Name = "modifiedDate")]
        public System.DateTime ModifiedDate { get; set; }
    }

    public class SupplierUserComparer : IEqualityComparer<SupplierUser>
    {
        public bool Equals(SupplierUser x, SupplierUser y)
        {
            return x.UserRef == y.UserRef
                   && x.FirstName == y.FirstName
                   && x.LastName == y.LastName
                   && x.Mail == y.Mail
                   && x.PartyRef == y.PartyRef;
        }

        public int GetHashCode(SupplierUser obj)
        {
            return obj.UserRef.GetHashCode();
        }
    }
}
