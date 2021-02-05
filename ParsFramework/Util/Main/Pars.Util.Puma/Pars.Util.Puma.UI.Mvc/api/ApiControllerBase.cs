using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Pars.Core;

namespace Pars.Util.Puma.UI.Mvc.api
{
    //public abstract class ApiControllerBase<TResponseBase> : ApiController where TResponseBase : new ()
    public abstract class ApiControllerBase : ApiController
    {
        public T ProxyOf<T>() where T : class
        {
            return new Core.Service.ProxyBase<T>(new Pars.Core.Security.FederationIdentityProvider()).Client;
        }

        //public static T ProxyOf<T>() where T : ResponseBase
        //{
        //    return new ProxyBase<T>(new Pars.Core.Security.FederationIdentityProvider()).Client;
        //}

        //public TResponseBase Execute<TResponseBase, TService>(Action<TResponseBase, TService> action, TResponseBase response = null) where TResponseBase : class, new() where TService : class
        //{
        //    response = response ?? new TResponseBase();
        //    try
        //    {
        //        action(response, ProxyOf<TService>());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.AddError(ex.GetMessageDeep());
        //    }

        //    return (response);
        //}
    }
}