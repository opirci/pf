using System.Collections.Generic;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IClaimRepository
    {
        ITQueryable<Claim> Claims { get; }

        //ITQueryable<Claim> GetClaims(IPaging paging);

        //ITQueryable<Claim> GetClaims(TextSearch search = null, IPaging paging = null);


        //List<Claim> GetClaims(string searchText);

        int Count(string searchText);

        //ITQueryable<Claim> GetClaims(string searchText, IPagingContext paging); // TODO PagedList

        //List<Claim> GetClaimsByName(string name);

        Claim GetClaimByRef(int claimRef);

        List<Claim> GetClaimsByUserRef(int userRef);

        ITQueryable<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging); // TODO PagedList

        Claim Save(Claim claim);
    }
}
