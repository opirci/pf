using System.Collections.Generic;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public interface IUserBusiness
    {
        ITQueryable<User> Users { get; }
        //List<LookupItem> SearchUser(string username);
        //List<User> GetUsers();
    }
}