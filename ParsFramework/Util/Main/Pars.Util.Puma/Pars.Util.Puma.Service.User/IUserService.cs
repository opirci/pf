using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.User
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {

        [OperationContract]
        UserSearchResponse SearchUser(UserSearchRequest request);

        [OperationContract]
        UserListResponse GetUsers();
    }
}
