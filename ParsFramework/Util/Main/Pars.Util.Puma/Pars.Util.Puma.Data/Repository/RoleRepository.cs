using System;
using System.Collections.Generic;
using System.Linq;
using Pars.Core;
using Pars.Core.Collection;
using Pars.Util.Puma.Domain;
using Pars.Core.Data;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{

    public class RoleRepository : IRoleRepository
    {
        private readonly IPumaUnitOfWork uow;
        private int languageRef => TApplication.Current.UserContext.User.LanguageRef;

        private const int PageSize = 20;


        public RoleRepository()
            : this(Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>())
        {

        }

        public RoleRepository(IPumaUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public ITQueryable<Role> Roles =>
            (from r in uow.Role.Table
             join es in uow.EntityState.Table on r.EntityStateRef equals es.EntityStateRef
             join esl in uow.EntityStateLocale.Table 
                on new { K1 = es.EntityStateRef, K2 = languageRef } equals new { K1 = esl.EntityStateRef.Value, K2 = esl.LanguageRef }
             let locale = r.tb_Role_Locale.FirstOrDefault()
             //let rolName = (locale != null && locale.Text != null) ? locale.Text : r.Name
             //orderby rolName
             orderby r.RoleRef
             select new Role
             {
                 Name = r.Name,
                 RoleRef = r.RoleRef,
                 EntityState = new EntityState
                 {
                     EntityStateRef = es.EntityStateRef,
                     Name = es.Name,
                     Description = esl.Text
                 },
                 LocaleData = new LocaleData
                 {
                     //DataEntityState = locale.EntityState,
                     LocaleRef = locale.RoleLocaleRef,
                     Value = locale.Text
                 }
             }).AsTQueryable();

        IQueryable<RoleRelationPre> GetRolesByUserRefQuery(int userRef)
            => from role in Roles
               from c in uow.UserRole.Table
               where c.UserRef == userRef && role.RoleRef == c.RoleRef
               orderby c.UserRoleRef
               select new RoleRelationPre
               {
                   Role = role,
                   MappedToRef = c.UserRef,
                   RelationRef = c.UserRoleRef,

                   CreatedDate = c.CreatedDate,
                   CreatedUserRef = c.CreatedUserRef,
                   ModifiedDate = c.ModifiedDate,
                   ModifiedUserRef = c.ModifiedUserRef
               };

        IQueryable<RoleRelationPre> GetRolesByUserGroupRefQuery(int userGroupRef)
            => from role in Roles
               from c in uow.UserGroupRole.Table
               where c.UserGroupRef == userGroupRef && role.RoleRef == c.RoleRef
               orderby c.UserGroupRoleRef
               select new RoleRelationPre
               {
                   Role = role,
                   MappedToRef = c.UserGroupRef,
                   RelationRef = c.UserGroupRoleRef,

                   CreatedDate = c.CreatedDate,
                   CreatedUserRef = c.CreatedUserRef,
                   ModifiedDate = c.ModifiedDate,
                   ModifiedUserRef = c.ModifiedUserRef
               };

        IQueryable<RoleRelationPre> GetRolesByClaimRefQuery(int claimRef)
            => from role in Roles
               from c in uow.RoleItem.Table
               where c.ClaimRef == claimRef && role.RoleRef == c.RoleRef
               orderby c.RoleItemRef
               //let mappedToRef = c.ClaimRef
               //let relationRef = c.RoleItemRef

               select new RoleRelationPre
               {
                   Role = role,
                   MappedToRef = c.ClaimRef,
                   RelationRef = c.RoleItemRef,

                   CreatedDate = c.CreatedDate,
                   CreatedUserRef = c.CreatedUserRef,
                   ModifiedDate = c.ModifiedDate,
                   ModifiedUserRef = c.ModifiedUserRef
               };

        //select new RoleRelation
        //{
        //    RelatedRef = mappedToRef,
        //    RelationRef = relationRef,
        //    Name = role.Name,
        //    RoleRef = role.RoleRef,
        //    LocaleData = new LocaleData
        //    {
        //        LocaleRef = role.LocaleData.LocaleRef,
        //        Value = role.LocaleData.Value
        //    },
        //    EntityState = new EntityState
        //    {
        //        EntityStateRef = role.EntityState.EntityStateRef,
        //        Name = role.EntityState.Name,
        //        Description = role.EntityState.Description
        //    }


        public class RoleRelationPre : Pars.Util.Puma.Domain.IAuditable
        {
            public int MappedToRef { get; set; }
            public int RelationRef { get; set; }
            public Role Role { get; set; }

            public RoleRelation ToRoleRelation(Role role, int mappedToRef, int relationRef,
                Pars.Util.Puma.Domain.IAuditable roleRelation)
            {
                return new RoleRelation
                {
                    RelatedRef = mappedToRef,
                    RelationRef = relationRef,
                    Name = role.Name,
                    RoleRef = role.RoleRef,
                    LocaleData = new LocaleData
                    {
                        LocaleRef = role.LocaleData.LocaleRef,
                        Value = role.LocaleData.Value
                    },
                    EntityState = new EntityState
                    {
                        EntityStateRef = role.EntityState.EntityStateRef,
                        Name = role.EntityState.Name,
                        Description = role.EntityState.Description
                    },
                    CreatedDate = roleRelation.CreatedDate,
                    CreatedUserRef = roleRelation.CreatedUserRef,
                    ModifiedDate = roleRelation.ModifiedDate,
                    ModifiedUserRef = roleRelation.ModifiedUserRef
                };
            }

            public DateTime? CreatedDate { get; set; }
            public int? CreatedUserRef { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public int? ModifiedUserRef { get; set; }
        }

        public int Count()
            => uow.Role.Table.Count();


        //public ITQueryable<Role> GetRoles(IPaging paging)
        //  => GetRoles(null, paging);

        //public ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null)
        //{
        //    IQueryable<Role> query = Roles.OrderBy(o => o.Name);
        //    //int queryCnt = 0;
        //    if (!TextSearch.IsNullOrEmpty(search))
        //    {
        //        query = query.AsTQueryable().Where(p => p.Name.Match(search.Text, search.SearchAs));
        //            //|| (p.LocaleData != null && p.LocaleData.Value != null && p.LocaleData.Value.Match(search.Text, search.SearchAs)));
        //    }
        //    //if (paging != null)
        //    //{
        //    //    queryCnt = query.Count();
        //    //    query = query.WithPaging(paging);
        //    //}

        //    //ITQueryable<Role> res = query.AsTQueryable();
        //    //if (paging != null) {
        //    //    res.TotalCount = queryCnt;
        //    //    res.PageNumber = paging.PageNumber;
        //    //    res.PageSize= paging.PageSize;
        //    //}

        //    ITQueryable<Role> res = query.AsTQueryable();
        //    if (paging != null)
        //    {
        //        res = res.WithPaging(paging);
        //    }

        //    return res;
        //}


        public IQueryable<RoleRelation> GetRolesByUserRef(int userRef)
            => GetRolesByUserRefQuery(userRef).AsRoleRelation();

        public IQueryable<RoleRelation> GetRolesByUserGroupRef(int userGroupRef)
            => GetRolesByUserGroupRefQuery(userGroupRef).AsRoleRelation();

        public IQueryable<RoleRelation> GetRolesByClaimRef(int claimRef)
            => GetRolesByClaimRefQuery(claimRef).AsRoleRelation();

        public ITQueryable<RoleRelation> GetRolesByUserRef(int userRef, IPaging paging)
            => GetRolesByUserRefQuery(userRef).AsRoleRelation()
                .AsTQueryable().WithPaging(paging);

        public ITQueryable<RoleRelation> GetRolesByUserGroupRef(int userGroupRef, IPaging paging)
            => GetRolesByUserGroupRefQuery(userGroupRef)
                .AsRoleRelation()
                .AsTQueryable().WithPaging(paging);

        public ITQueryable<RoleRelation> GetRolesByClaimRef(int claimRef, IPaging paging, TextSearch search = null)
        {
            IQueryable<RoleRelation> res = GetRolesByClaimRefQuery(claimRef).AsRoleRelation();

            if (!TextSearch.IsNullOrEmpty(search))
                res = res.AsTQueryable().Where(rl => rl.Name.Match(search.Text, search.SearchAs));

            return res.AsTQueryable().WithPaging(paging);
        }

        public Role Save(Role role)
        {
            throw new NotImplementedException();
        }

        public void Save(List<Role> roles)
        {
            throw new NotImplementedException();
        }

        public RoleRelation SaveRoleClaim(RoleRelation roleRelation)
        {
            throw new NotImplementedException();
        }

        public RoleRelation SaveRoleUser(RoleRelation roleRelation)
        {
            throw new NotImplementedException();
        }

        public RoleRelation SaveRoleUserGroup(RoleRelation roleRelation)
        {
            throw new NotImplementedException();
        }

        public void SaveRoleClaim(List<RoleRelation> roleMap)
        {
            throw new NotImplementedException();
        }

        public void SaveRoleUser(List<RoleRelation> roleMap)
        {
            throw new NotImplementedException();
        }

        public void SaveRoleUserGroup(List<RoleRelation> roleMap)
        {
            throw new NotImplementedException();
        }

    }

    public static class RoleExtensions
    {
        public static IQueryable<RoleRelation> AsRoleRelation(
                this IQueryable<RoleRepository.RoleRelationPre> instance)
            => instance.Select(r => new RoleRelation
            {
                RelatedRef = r.MappedToRef,
                RelationRef = r.RelationRef,
                Name = r.Role.Name,
                RoleRef = r.Role.RoleRef,
                LocaleData = new LocaleData
                {
                    LocaleRef = r.Role.LocaleData.LocaleRef,
                    Value = r.Role.LocaleData.Value
                },
                EntityState = new EntityState
                {
                    EntityStateRef = r.Role.EntityState.EntityStateRef,
                    Name = r.Role.EntityState.Name,
                    Description = r.Role.EntityState.Description
                },
                CreatedDate = r.CreatedDate,
                CreatedUserRef = r.CreatedUserRef,
                ModifiedDate = r.ModifiedDate,
                ModifiedUserRef = r.ModifiedUserRef
            }
            );

        //List <RoleRelation> list = new List<RoleRelation>();
        //instance.ForEach(i => list.Add(i.ToRoleRelation(i.Role, i.RelatedRef, i.RelationRef, i)));
        //return list.AsQueryable();
    }
}
