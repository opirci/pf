using System.Linq;
using Pars.Core;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private const int PageSize = 20;
        private readonly IPumaUnitOfWork _unitOfWork;

        public UserRepository() : this(Container.Instance.Resolve<IPumaUnitOfWork>())
        {

        }

        public UserRepository(IPumaUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ITQueryable<User> Users => 
            (from user in _unitOfWork.User.Table
             //join es in _unitOfWork.EntityState.Table on user.en equals es.EntityStateRef
             select new User()
                {
                    Domain = user.Domain,
                    IsDomainUser = user.IsDomainUser ?? false,
                    UserRef = user.UserRef,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    Surname = user.SurName,
                    HrPersonelRef = user.HRPersonelRef ?? 0,
                    //DataEntityState = user.EntityState
             }).AsTQueryable();

        //public ITQueryable<User> GetUsers(IPaging paging)
        //  => GetUsers(null, paging);

        //public ITQueryable<User> GetUsers(TextSearch search = null, IPaging paging = null)
        //{
        //    IQueryable<User> query = Users.OrderBy(u => u.Username);
        //    if (!TextSearch.IsNullOrEmpty(search))
        //    {
        //        query = query.AsTQueryable().Where(p => p.Username.Match(search.Text, search.SearchAs));
        //    }
            
        //    ITQueryable<User> res = query.AsTQueryable();
        //    if (paging != null)
        //    {
        //        res = res.WithPaging(paging);
        //    }

        //    return res;
        //}

        //public List<LookupItem> SearchUser(string username)
        //{
        //    var users = Users.Where(w => w.Username.StartsWith(username));
        //    List<LookupItem> result = users.Select(s => new LookupItem() { Ref = s.UserRef, Name = s.Username }).ToList();
        //    return result;
        //}

        //public List<User> GetUsers()
        //{
        //    List<User> result = new List<User>();
        //    result = Users.ToList();
        //    return result;
        //}

        public PagedList<UserRelation> GetUserRelationByClaimRef(int claimRef, IPaging paging)
        {
            var users = 
                (from userClaim in _unitOfWork.UserClaim.Table
                    join user in _unitOfWork.User.Table on userClaim.UserRef equals user.UserRef
                    join ms in _unitOfWork.MemberState.Table on userClaim.MemberStateRef equals ms.MemberStateRef
                    where userClaim.ClaimRef == claimRef
                    select new UserRelation()
                    {
                        Username = user.UserName,
                        UserRef = userClaim.UserRef,
                        MemberState = new MemberState()
                        {
                            MemberStateRef = ms.MemberStateRef,
                            Name = ms.tb_MemberState_Locale.FirstOrDefault().Text
                        },
                        ValidFrom = userClaim.ValidFrom.Value,
                        ValidTo = userClaim.ValidTo.Value
                    })
                    .OrderBy(o => o.Username).AsPagedList(paging);

            return users;
        }

        public UserRelation SaveUserClaimRelation(UserRelation user)
        {
            tb_UserClaim userClaim = new tb_UserClaim()
            {
                UserClaimRef = user.RelationRef,
                UserRef = user.UserRef,
                ClaimRef = user.RelatedRef,
                MemberStateRef = user.MemberState.MemberStateRef,
                ValidFrom = user.ValidFrom,
                ValidTo = user.ValidTo,
                EntityState = user.EntityState
            };
            _unitOfWork.UserClaim.Enroll(userClaim);
            _unitOfWork.Save();
            user.RelationRef = userClaim.UserClaimRef;
            return user;
        }
    }
}
