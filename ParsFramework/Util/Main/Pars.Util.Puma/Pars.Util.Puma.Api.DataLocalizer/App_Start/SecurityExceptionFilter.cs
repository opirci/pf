using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Http.Filters;

namespace Pars.Util.Puma.Api.DataLocalizer
{
    public class SecurityExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is SecurityException)
            {
                // log exception details

                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(actionExecutedContext.Exception.Message);

                actionExecutedContext.Response = response;
            }
        }

    }
}