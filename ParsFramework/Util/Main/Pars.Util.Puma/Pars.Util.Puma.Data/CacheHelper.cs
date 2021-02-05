using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Caching;
using Pars.Core.Caching.Redis;

namespace Pars.Util.Puma.Data
{
    public static class CacheHelper
    {
        private static readonly Lazy<ICacheProvider> _cacheProvider;

        private static ICacheProvider CacheProvider => _cacheProvider.Value;

        static CacheHelper()
        {
            _cacheProvider = new Lazy<ICacheProvider>(() => (RedisCacheProvider)(Pars.Core.Container.Instance.Resolve<ICacheProvider>()));
        }

        public static List<TEntity> Find<TEntity>(Func<TEntity, bool> predicate)
        {
            string typeName = String.Format("{0}:{1}",
                ConfigurationManager.AppSettings["CacheBaseKey"],
                typeof(TEntity).FullName);

            List<TEntity> entities = CacheProvider.Get<List<TEntity>>(typeName).ToList();

            return entities.Where(predicate).ToList();
        }
    }
}
