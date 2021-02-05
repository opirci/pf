using System.Linq;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Data.Repository
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly IPumaUnitOfWork _unitOfWork;

        private const int PageSize = 20;

        public UserGroupRepository()
           : this(Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>())
        {

        }

        public UserGroupRepository(IPumaUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ITQueryable<UserGroup> UserGroups =>
            (from userGroup in _unitOfWork.UserGroup.Table
             join es in _unitOfWork.EntityState.Table on userGroup.EntityStateRef equals es.EntityStateRef
             let locale = userGroup.tb_UserGroup_Locale.FirstOrDefault()
             //let ugName = (locale != null && locale.Text != null) ? locale.Text : userGroup.Name
             //orderby ugName
             orderby userGroup.UserGroupRef
             select new UserGroup()
             {
                 UserGroupRef = userGroup.UserGroupRef,
                 Name = userGroup.Name,
                 EntityState = new EntityState()
                 {
                     EntityStateRef = es.EntityStateRef,
                     Name = es.Name,
                     Description = es.tb_EntityState_Locale.FirstOrDefault().Text
                 },
                 LocaleData = new LocaleData
                 {
                     //DataEntityState = locale.EntityState,
                     LocaleRef = locale.UserGroupLocaleRef,
                     Value = locale.Text
                 }
             }).AsTQueryable();

        //public IQueryable<LookupItem> UserGroupLookups =>
        //   from userGroup in _unitOfWork.UserGroup.Table
        //   let locale = userGroup.tb_UserGroup_Locale.FirstOrDefault()
        //   let lk = new LookupItem()
        //   {
        //       Ref = userGroup.UserGroupRef,
        //       Name = (locale != null) ? locale.Text : userGroup.Name
        //   }
        //   orderby lk.Name
        //   select lk;

        //public ITQueryable<UserGroup> GetUserGroups(IPaging paging)
        //    => GetUserGroups(null, paging);

        //public ITQueryable<UserGroup> GetUserGroups(TextSearch search = null, IPaging paging = null)
        //{
        //    IQueryable<UserGroup> query = UserGroups.OrderBy(u => u.Name);
        //    if (!TextSearch.IsNullOrEmpty(search))
        //    {
        //        query = query.AsTQueryable().Where(p => p.Name.Match(search.Text, search.SearchAs));
        //        //|| (p.LocaleData.Value.Match(search.Text, search.SearchAs)));
        //    }

        //    ITQueryable<UserGroup> res = query.AsTQueryable();
        //    if (paging != null)
        //    {
        //        res = res.WithPaging(paging);
        //    }

        //    return res;
        //}


        //public ITQueryable<LookupItem> GetUserGroupsAsLookup(IPaging paging)
        //   => GetUserGroupsAsLookup(null, paging);

        //public ITQueryable<LookupItem> GetUserGroupsAsLookup(TextSearch search = null, IPaging paging = null)
        //{
        //    ITQueryable<LookupItem> query = UserGroupLookups.AsTQueryable();
        //    if (!TextSearch.IsNullOrEmpty(search))
        //    {
        //        query = query.Where(p => p.Name.Match(search.Text, search.SearchAs));
        //    }

        //    if (paging != null)
        //    {
        //        query = query.WithPaging(paging);
        //    }

        //    return query;
        //}

        public PagedList<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging)
        {
            PagedList<UserGroupRelation> userGroups =
                (from ug in _unitOfWork.UserGroup.Table
                 join ugc in _unitOfWork.UserGroupClaim.Table on ug.UserGroupRef equals ugc.UserGroupRef
                 join ms in _unitOfWork.MemberState.Table on ugc.MemberStateRef equals ms.MemberStateRef
                 where ugc.ClaimRef == claimRef
                 orderby ug.UserGroupRef
                 select new UserGroupRelation()
                 {
                     UserGroupRef = ug.UserGroupRef,
                     Name = ug.Name,
                     Description = ug.tb_UserGroup_Locale.FirstOrDefault().Text,
                     ValidFrom = ugc.ValidFrom.Value,
                     ValidTo = ugc.ValidTo.Value,
                     MemberState = new MemberState()
                     {
                         MemberStateRef = ugc.MemberStateRef.Value,
                         Name = ms.tb_MemberState_Locale.FirstOrDefault().Text
                     }
                     //}).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                 }).AsPagedList(paging);

            return userGroups;
        }

        public int Count(int claimRef)
        {
            return _unitOfWork.UserGroupClaim.Count(ug => ug.ClaimRef == claimRef);
        }
    }
}
