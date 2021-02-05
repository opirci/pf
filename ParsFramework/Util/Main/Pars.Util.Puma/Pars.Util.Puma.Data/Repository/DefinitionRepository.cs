using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pars.Core.Caching;
using Pars.Core.Security;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public class DefinitionRepository : IDefinitionRepository
    {
        private readonly Lazy<IDefinitionRepository> _repository;
        private readonly Lazy<ICacheProvider> _cacheProviderLazy;

        private ICacheProvider CacheProvider => _cacheProviderLazy.Value;
        private IDefinitionRepository Repository => _repository.Value;

        private IIdentityProvider _identityProvider;

        public DefinitionRepository()
        {
            _repository = new Lazy<IDefinitionRepository>(() => (IDefinitionRepository)Pars.Core.Container.Instance.Resolve(typeof(IDefinitionRepository), "DefinitionRepositoryInternal"), LazyThreadSafetyMode.ExecutionAndPublication);
            _cacheProviderLazy = new Lazy<ICacheProvider>(() => Pars.Core.Container.Instance.Resolve<ICacheProvider>(), LazyThreadSafetyMode.ExecutionAndPublication);

            _identityProvider = Pars.Core.Container.Instance.Resolve<IIdentityProvider>();

        }

        public DefinitionRepository(IDefinitionRepository repository, ICacheProvider cacheProvider, IIdentityProvider identityProvider)
        {
            _repository = new Lazy<IDefinitionRepository>(() => repository);
            _cacheProviderLazy = new Lazy<ICacheProvider>(() => cacheProvider);
            this._identityProvider = identityProvider;
        }

        public List<EntityState> GetEntityStates()
        {
            string key =
                $"{ConfigurationManager.AppSettings["CacheBaseKey"]}Definition:tb_EntityState:{_identityProvider.LanguageRef.ToString()}";
            List<EntityState> result = CacheProvider.Get<List<EntityState>>(key);
            if (result == null)
            {
                result = Repository.GetEntityStates();
                CacheProvider.Set(key, result);
            }
            return result;
        }

        private string cacheKeyFormat = null;

        string GenerateCacheKey(string key)
        {
            cacheKeyFormat = cacheKeyFormat ??
                             (cacheKeyFormat =
                                 string.Concat(ConfigurationManager.AppSettings["CacheBaseKey"], "{0}:", _identityProvider.LanguageRef.ToString()));
            return string.Format(cacheKeyFormat, key);
        }

        public List<MemberState> GetMemberStates()
        {
            //string key =
            //    $"{ConfigurationManager.AppSettings["CacheBaseKey"]}Definition:tb_MemberState:{_identityProvider.LanguageRef.ToString()}";

            //string key = GenerateCacheKey("Definition:tb_MemberState");

            //List<MemberState> result = CacheProvider.Get<List<MemberState>>(key);

            //if (result == null)
            //{
            //    result = Repository.GetMemberStates();
            //    CacheProvider.Set(key, result);
            //}
            //return result;
            return GetWithKey("Definition:tb_MemberState", () => Repository.GetMemberStates());
        }

        T GetWithKey<T>(string keys, Func<T> getFunc)
        {
            string key = GenerateCacheKey(keys);

            T result = CacheProvider.Get<T>(key);

            if (result == null)
            {
                result = getFunc();
                CacheProvider.Set(key, result);
            }
            return result;

        }

        public List<LookupItem> GetSegmentsAsLookup()
            => GetWithKey("Definition:tb_Segments_AsLookup", () => Repository.GetSegmentsAsLookup());

        public List<LookupItem> GetSubSegmentsAsLookup()
            => GetWithKey("Definition:tb_SubSegments_AsLookup", () => Repository.GetSubSegmentsAsLookup());

        public List<LookupItem> GetBusinessLinesAsLookup()
            => GetWithKey("Definition:tb_BusinessLines_AsLookup", () => Repository.GetBusinessLinesAsLookup());

        public List<LookupItem> GetAgeSexGroupsAsLookup()
            => GetWithKey("Definition:tb_AgeSexGroup_AsLookup", () => Repository.GetAgeSexGroupsAsLookup());

        public List<LookupItem> GetSupplierTypesAsLookup()
            => GetWithKey("Definition:tb_SupplierTypes_AsLookup", () => Repository.GetSupplierTypesAsLookup());

        public List<LookupItem> GetServers()
            => GetWithKey("Definition:tb_Server", () => Repository.GetServers());

        public List<LookupItem> GetDatabases(int serverRef)
            => Repository.GetDatabases(serverRef);

        public List<LookupItem> GetTables(int databaseRef)
            => Repository.GetTables(databaseRef);

        public List<LookupItem> GetTableColumns(int tableRef)
            => Repository.GetTableColumns(tableRef);
    }
}
