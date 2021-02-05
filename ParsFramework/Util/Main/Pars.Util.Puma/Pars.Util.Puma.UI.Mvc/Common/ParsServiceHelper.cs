using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pars.Core.Security;
using Pars.Util.Puma.UI.Mvc.ParsMail;
using Pars.Util.Puma.UI.Mvc.ParsMailTemplate;

namespace Pars.Util.Puma.UI.Mvc.Common
{
    public class ParsServiceHelper
    {
        private readonly IIdentityProvider _identityProvider;

        public ParsServiceHelper() : this(new FederationIdentityProvider())
        {
            
        }
        public ParsServiceHelper(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public void SendMail(MailSendRequest mailSendRequest)
        {
            var mailService = new Pars.Core.Service.ProxyBase<ParsMail.IEmailService>(_identityProvider);
            mailService.Client.SendMail(mailSendRequest);
        }

        public MailTemplateGetResponse GetMailTemplate(MailTemplateGetRequest mailTemplateGetRequest)
        {
            mailTemplateGetRequest.LanguageRef = _identityProvider.LanguageRef;
            var mailTemplate = new Pars.Core.Service.ProxyBase<ParsMailTemplate.IEmailTemplateService>(_identityProvider);
            return mailTemplate.Client.GetMailTemplate(mailTemplateGetRequest);
        }

        public MailTemplateReplaceParametersWithValueResponse GetMailTemplateWithReplacedValues(
            MailTemplateReplaceParametersWithValueRequest request)
        {
            var mailTemplate = new Pars.Core.Service.ProxyBase<ParsMailTemplate.IEmailTemplateService>(_identityProvider);
            return mailTemplate.Client.GetMailTemplateWithValues(request);
        }

        public Guid CreatePasswordRequest(string mailAddress)
        {
            var userSvc = new Pars.Core.Service.ProxyBase<ParsUser.IUserServicePublic>(_identityProvider);
            return userSvc.Client.CreatePasswordResetRequest(mailAddress);
        }
    }
}