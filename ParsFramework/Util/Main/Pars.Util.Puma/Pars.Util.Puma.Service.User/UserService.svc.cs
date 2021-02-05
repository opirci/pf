using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Core.Linq;
using System.Linq;

namespace Pars.Util.Puma.Service.User
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        IUserBusiness _userBusiness;
        ISupplierBusiness _supplierBusiness;
        public UserService(IUserBusiness business, ISupplierBusiness supplierBusiness)
        {
            _userBusiness = business;
            _supplierBusiness = supplierBusiness;
        }

        public UserService() : this(Container.Instance.Resolve<IUserBusiness>("UserBusiness"),
            Container.Instance.Resolve<ISupplierBusiness>("SupplierBusiness")
            )
        {

        }

        public UserSearchResponse SearchUser(UserSearchRequest request)
        {
            UserSearchResponse result = new UserSearchResponse();
            result.Users = _userBusiness.Users.GetItems(u => u.Username, new TextSearch(request.SearchText)).AsLookup().ToList();
            return result;
        }

        public UserListResponse GetUsers()
        {
            UserListResponse result = new UserListResponse();
            result.Users = _userBusiness.Users.GetItems().ToList();
            return result;
        }
    }
}
