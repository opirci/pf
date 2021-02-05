
using System.Collections.Generic;
using System.Web.Http;

namespace Pars.Util.Puma.Api.AuthManagement.Controllers
{
    [Authorize]
    public class FederationController : ApiController
    {
        [HttpGet]
        [Route("api/Federation/UserContext")]
        public UserContext UserContext()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();

            return new UserContext
            {
                userName = idp.UserName,
                culture = idp.Culture,
                languageRef = idp.LanguageRef,
                hrPersonelRef = idp.HRPersonelRef,
                domain = idp.DomainName,
                userRef = idp.UserRef,
                claims = idp.Claims.ToArray()
            };
        }

        [HttpGet]
        public string UserName()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.UserName;
        }

        [HttpGet]
        public string Culture()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            string culture = idp.Culture;
            return culture;
        }

        [HttpGet]
        public string LanguageRef()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.LanguageRef.ToString();
        }
        [HttpGet]
        public string HrPersonelRef()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.HRPersonelRef.ToString();
        }
        [HttpGet]
        public string Domain()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.DomainName;
        }
        [HttpGet]
        public List<string> Claims()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.Claims;
        }

        [HttpGet]
        public string UserRef()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            return idp.UserRef.ToString();
        }
    }
    public class UserContext
    {
        public string userName { get; set; }
        public string culture { get; set; }
        public int languageRef { get; set; }
        public int hrPersonelRef { get; set; }
        public string domain { get; set; }
        public string[] claims { get; set; }
        public int userRef { get; set; }
    }
}
