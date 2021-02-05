using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.UI.Mvc.Common
{
    public class UserBasicInfo
    {
        public int UserRef { get; set; }
        public int UserLogonRef { get; set; }
        public string UserName { get; set; }
        public bool IsDomainLogon { get; set; }
        public string DomainName { get; set; }
        public string PwdHash { get; set; }
        public Nullable<System.DateTime> PwdExpiryDate { get; set; }
        public string MailAddress { get; set; }
        public Nullable<int> UserMailRef { get; set; }
        public System.Guid UserUid { get; set; }
        public bool CanLogin { get; set; }
        public string SupplierCode { get; set; }
        public Nullable<int> HRPersonelRef { get; set; }
        public int CompanyRef { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string MobileAreaCode { get; set; }
        public string Mobile { get; set; }
        public int CreatedUserRef { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedUserRef { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}