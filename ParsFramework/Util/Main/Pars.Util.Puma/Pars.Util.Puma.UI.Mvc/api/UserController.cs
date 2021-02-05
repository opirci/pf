using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Pars.Util.Puma.Domain;
using Pars.Util.Puma.UI.Mvc.SupplierSvc;
using Pars.Util.Puma.UI.Mvc.UserSvc;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class UserController : ApiControllerBase
    {
        //[HttpGet]
        //public IEnumerable<LookupItem> SearchUser(string searchText)
        //{
        //    var idp = new Pars.Core.Security.FederationIdentityProvider();
        //    var userService = new Pars.Core.Service.ProxyBase<UserSvc.IUserService>(idp);
        //    var users = userService.Client.SearchUser(new UserSearchRequest() { SearchText = searchText });
        //    return users.Users.AsEnumerable();
        //}
    }
}