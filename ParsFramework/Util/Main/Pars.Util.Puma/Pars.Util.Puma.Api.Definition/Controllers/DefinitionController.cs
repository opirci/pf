using Pars.Util.Puma.Api.Definition.Models;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Util.Puma.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Pars.Util.Puma.Api.Definition.Controllers
{
    [Authorize]
    public class DefinitionController : ApiController
    {
        public DefinitionController()
        {
            LookupItem.SetDefaultMap<User>(l => l.UserRef, l => l.Username);
            LookupItem.SetDefaultMap<UserGroup>(l => l.UserGroupRef, l => l.Name);
            LookupItem.SetDefaultMap<Role>(l => l.RoleRef, l => l.Name);
            LookupItem.SetDefaultMap<Claim>(l => l.ClaimRef, l => l.Name);
        }

        readonly DefinitionBusiness business = new DefinitionBusiness();

        [HttpGet]
        [Route("api/definition/entitystates")]
        public GetEntityStatesResponse GetEntityStates()
        {
            GetEntityStatesResponse result = new GetEntityStatesResponse() { EntityStates = business.GetEntityStates() };
            return result;
        }

        [HttpGet]
        [Route("api/definition/memberstates")]
        public MemberStateListResponse GetMemberStates()
        {
            MemberStateListResponse reponse = new MemberStateListResponse()
            {
                MemberStates = business.GetMemberStates()
            };

            return reponse;
        }

        [HttpGet]
        [Route("api/definition/servers")]

        public LookupResponse GetServers()
           => ResponseBase.Execute<LookupResponse>(r =>
           {
               var result = business.GetServers();
               r.Values = new List<LookupItem>(result);
           });

        [HttpPost]
        [Route("api/definition/databases")]
        public LookupResponse GetDatabases([FromUri]GetDatabasesRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = business.GetDatabases(request.ServerRef);
                r.Values = new List<LookupItem>(result);
            });

        [HttpPost]
        [Route("api/definition/tables")]
        public LookupResponse GetTables([FromUri]GetTablesRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = business.GetTables(request.DatabaseRef);
                r.Values = new List<LookupItem>(result);
            });

        [HttpPost]
        [Route("api/definition/tablecolumns")]
        public LookupResponse GetTableColumns([FromUri]GetTableColumnsRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = business.GetTableColumns(request.TableRef);
                r.Values = new List<LookupItem>(result);
            });

        [HttpGet]
        [Route("api/definition/languages")]
        public LookupResponse GetLanguages()
          => ResponseBase.Execute<LookupResponse>(r =>
          {
              var result = business.GetLanguages();
              r.Values = new List<LookupItem>(result);
          });

        [HttpPost]
        [Route("api/definition/roles")]
        public PagedLookupResponse Roles([FromBody]LookupRequest request)
            => Get(request, LookupType.Roles);

        [HttpPost]
        [Route("api/definition/users")]
        public PagedLookupResponse Users([FromBody]LookupRequest request)
            => Get(request, LookupType.Users);      

        [HttpPost]
        [Route("api/definition/usergroups")]
        public PagedLookupResponse UserGroups([FromBody]LookupRequest request)
            => Get(request, LookupType.UserGroups);

        [HttpPost]
        [Route("api/definition/claims")]
        public PagedLookupResponse Claims([FromBody]LookupRequest request)
            => Get(request, LookupType.Claims);     

        PagedLookupResponse Get(LookupRequest request, LookupType lookup)
            => ResponseBase.Execute<PagedLookupResponse>(r =>
            {
                ITQueryable<LookupItem> res = null;                

                switch (lookup)
                {
                    case LookupType.Users:
                        res = business.Users.GetItems(u => u.Username, request.Search, request.Paging).AsLookup();
                        //res = business.GetUsers(request.Search, request.Paging).AsLookup();
                        break;
                    case LookupType.UserGroups:
                        res = business.UserGroups.GetItems(u => u.Name, request.Search, request.Paging).AsLookup();
                        //res = business.GetUserGroups(request.Search, request.Paging).AsLookup();                       
                        break;
                    case LookupType.Roles:
                        res = business.Roles.GetItems(u => u.Name, request.Search, request.Paging).AsLookup();
                        //res = business.GetRoles(request.Search, request.Paging).AsLookup();
                        break;
                    case LookupType.Claims:
                        res = business.Claims.GetItems(u => u.Name, request.Search, request.Paging).AsLookup();
                        //res = business.GetClaims(request.Search, request.Paging).AsLookup();
                        break;
                }
                r.Values = res.ToList();
                r.PageNumber = res.PageNumber;
                r.PageSize = res.PageSize;
                r.TotalCount = res.TotalCount;
            });

        public enum LookupType
        {
            Users,
            UserGroups,
            Roles,
            Claims
        }
    }
}
