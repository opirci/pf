using System.Collections.Generic;
using System.Linq;
using Pars.Core.Data;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly IPumaUnitOfWork unitOfwork;

        private const int PageSize = 20;

        public ClaimRepository() :
            this(Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>())
        {

        }

        public ClaimRepository(IPumaUnitOfWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public ITQueryable<Claim> Claims =>
            (from c in unitOfwork.Claim.Table
             join es in unitOfwork.EntityState.Table on c.EntityStateRef equals es.EntityStateRef
             let locale = c.tb_Claim_Locale.FirstOrDefault()
             //let claimName = (locale != null && locale.Description != null) ? locale.Description : c.Name
             //orderby claimName
             orderby c.ClaimRef
             select new Claim()
             {
                 ClaimRef = c.ClaimRef,
                 Name = c.Name,
                 EntityState = new EntityState()
                 {
                     EntityStateRef = es.EntityStateRef,
                     Name = es.Name,
                     Description = es.tb_EntityState_Locale.FirstOrDefault().Text
                 },
                 LocaleData = new LocaleData
                 {
                     //DataEntityState = locale.EntityState,
                     LocaleRef = locale.ClaimLocaleRef,
                     Value = locale.Description
                 }
             }).AsTQueryable();      

        //public ITQueryable<Claim> GetClaims(IPaging paging)
        //    => GetClaims(null, paging);


        //public ITQueryable<Claim> GetClaims(TextSearch search = null, IPaging paging = null)
        //{
        //    IQueryable<Claim> query = Claims.OrderBy(c => c.Name);
        //    if (!TextSearch.IsNullOrEmpty(search))
        //    {
        //        query = query.AsTQueryable()
        //            .Where(p => p.Name.Match(search.Text, search.SearchAs));
        //            //|| (p.LocaleData != null && p.LocaleData.Value != null && p.LocaleData.Value.Match(search.Text, search.SearchAs)));
        //    }

        //    ITQueryable<Claim> res = query.AsTQueryable();
        //    if (paging != null)
        //    {
        //        res = res.WithPaging(paging);
        //    }

        //    return res;
        //}

        //public List<Claim> GetClaims(string searchText)
        //{
        //    var result = string.IsNullOrEmpty(searchText) ? Claims.ToList() : Claims.Where(x => x.Name.Contains(searchText) || x.LocaleData.Value.Contains(searchText)).ToList();
        //    return result;
        //}

        //public List<Claim> GetClaims(string searchText, IPagingContext paging)
        //{
        //    var result = string.IsNullOrEmpty(searchText)
        //        ? Claims.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList()
        //        : Claims.Where(x => x.Name.Contains(searchText) || x.LocaleData.Value.Contains(searchText)).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();

        //    return result;
        //}

        //public List<Claim> GetClaimsByName(string name)
        //{
        //    return Claims.Where(c => c.Name == name || (c.LocaleData != null && c.LocaleData.Value == name)).ToList();
        //}

        public Claim GetClaimByRef(int claimRef)
        {
            Claim claim = Claims.FirstOrDefault(c => c.ClaimRef == claimRef);

            return claim;
        }


        public List<Claim> GetClaimsByUserRef(int userRef)
        {
            List<Claim> result = (from c in Claims
                                  join uc in unitOfwork.UserClaim.Table on c.ClaimRef equals uc.ClaimRef
                                  where uc.UserRef == userRef
                                  select c).ToList();
            return result;
        }

        public int Count(string searchText)
        {
            return string.IsNullOrEmpty(searchText)
                ? unitOfwork.Claim.Table.Count()
                : Claims.Count(x => x.Name.Contains(searchText) || x.LocaleData.Value.Contains(searchText));
        }

        public ITQueryable<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging)
        {
            ITQueryable<UserGroupRelation> userGroups =
                (from ug in unitOfwork.UserGroup.Table
                    join ugc in unitOfwork.UserGroupClaim.Table on ug.UserGroupRef equals ugc.UserGroupRef
                    join ms in unitOfwork.MemberState.Table on ugc.MemberStateRef equals ms.MemberStateRef
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
                            Name = ms.Name
                        }
                    })//.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            .AsTQueryable().WithPaging(paging);

            return userGroups;
        }

        public Claim Save(Claim claim)
        {
            var tbClaim = new tb_Claim()
            {
                ClaimRef = claim.Ref,
                EntityStateRef = claim.EntityState.EntityStateRef,
                Name = claim.Name,
                EntityState = claim.DataEntityState
            };

            tbClaim.tb_Claim_Locale.Add(new tb_Claim_Locale()
            {
                LanguageRef = unitOfwork.IdentityProvider.LanguageRef,
                ClaimRef = claim.ClaimRef,
                ClaimLocaleRef = claim.LocaleData.LocaleRef,
                Description = claim.LocaleData.Value,
                EntityState = claim.LocaleData.DataEntityState
            });

            unitOfwork.Claim.Enroll(tbClaim);
            unitOfwork.Save();

            claim.ClaimRef = tbClaim.ClaimRef;
            if (tbClaim.tb_Claim_Locale.Any())
            {
                claim.LocaleData.LocaleRef = tbClaim.tb_Claim_Locale.FirstOrDefault().ClaimLocaleRef;
            }
            return claim;
        }



    }
}
