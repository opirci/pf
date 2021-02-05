using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using Thinktecture.IdentityModel.Tokens.Http;

namespace Pars.Util.Puma.Api.DataLocalizer
{
    public class SecurityConfig
    {
        public static void ConfigureGlobal(HttpConfiguration globalConfig)
        {
            globalConfig.MessageHandlers.Add(new AuthenticationHandler(CreateConfiguration()));
            globalConfig.Filters.Add(new SecurityExceptionFilter());
        }
        public static AuthenticationConfiguration CreateConfiguration()
        {
            var config = new AuthenticationConfiguration();
            config.RequireSsl = false;
            config.AddJsonWebToken(ConfigurationManager.AppSettings["stsurl"], ConfigurationManager.AppSettings["scope"], ConfigurationManager.AppSettings["signingkey"]);

            return config;
        }
    }
}