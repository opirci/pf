using System.Web;
using System.Web.Mvc;

namespace Pars.Util.Puma.Api.DataLocalizer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
