using System.Web;
using System.Web.Mvc;

namespace Pars.Util.Puma.ESB.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
