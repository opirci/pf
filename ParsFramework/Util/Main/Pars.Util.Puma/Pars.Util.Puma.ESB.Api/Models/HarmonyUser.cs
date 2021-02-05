
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.ESB.Api.Models
{
    public class HarmonyUserParent
    {
        public HarmonyUser HarmonyUser { get; set; }
    }

    public class HarmonyUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HrPersonelRef { get; set; }
        public string Email { get; set; }
        public int AuthenticationType { get; set; }
        public string SuggestedUsername { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return String.Format("FirstName: {0}, LastName:{1}, HrPersonelRef:{2}, Email:{3}, AuthenticationType:{4}, PhoneNumber:{5}", FirstName, LastName, HrPersonelRef, Email, AuthenticationType, PhoneNumber);
        }
    }

    public enum AuthenticationType
    {
        UNDEFINED = 0,
        DOMAIN = 1,
        FORM = 2
    }
}