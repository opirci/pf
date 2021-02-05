using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IUserRepository
    {
        ITQueryable<User> Users { get; }
        //ITQueryable<User> GetUsers(IPaging paging);
        //ITQueryable<User> GetUsers(TextSearch search = null, IPaging paging = null);
        //List<LookupItem> SearchUser(string username);
        //List<User> GetUsers();
        PagedList<UserRelation> GetUserRelationByClaimRef(int claimRef, IPaging paging);
        UserRelation SaveUserClaimRelation(UserRelation user);
    }
}
