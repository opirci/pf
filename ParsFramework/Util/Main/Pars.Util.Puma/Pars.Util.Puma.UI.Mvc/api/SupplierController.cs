using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using Pars.Core.Security;
using Pars.Util.Puma.UI.Mvc.Common;
using Pars.Util.Puma.UI.Mvc.ParsMail;
using Pars.Util.Puma.UI.Mvc.ParsMailTemplate;
using Pars.Util.Puma.UI.Mvc.SupplierSvc;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class SupplierController : ApiControllerBase
    {
        [HttpGet]
        public GetSupplierReportResponse GetSupplierReport([FromUri]GetSupplierReportRequest request)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            {
                GetSupplierReportResponse resp = new GetSupplierReportResponse();
                resp.errorMessages = new[] { "Tedport admin privilages needed to execute this service!" };
                return resp;
            }
            var rs = ProxyOf<ISupplierService>().GetSupplierReport(request);

            return rs;
        }

        [HttpGet]
        public GetSupplierUserLoginReportResponse GetSupplierUserLoginReport([FromUri]GetSupplierUserLoginReportRequest request)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            {
                GetSupplierUserLoginReportResponse resp = new GetSupplierUserLoginReportResponse();
                resp.errorMessages = new[] { "Tedport admin privilages needed to execute this service!" };
                return resp;
            }

            //request = new GetSupplierUserLoginReportRequest { range = GetDateWeekRange(), reportType = 1 };
            var rs = ProxyOf<ISupplierService>().GetSupplierUserLoginReport(request);

            return rs;
        }

        [HttpGet]
        public GetSuppliersFilteredResponse GetSuppliersFiltered([FromUri]GetSuppliersFilteredRequest request)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            {
                GetSuppliersFilteredResponse resp = new GetSuppliersFilteredResponse();
                resp.errorMessages = new[] { "Tedport admin privilages needed to execute this service!" };
                return resp;
            }
            return ProxyOf<ISupplierService>().GetSuppliersFiltered(request);
        }

        [HttpGet]
        public GetSupplierResponse GetSupplier([FromUri]GetSupplierRequest request)
        {
            var response = new GetSupplierResponse();


            if (!HasClaims(response))
            {
                return response;
            }


            response = ProxyOf<ISupplierService>().GetSupplier(request);

            var idp = new FederationIdentityProvider();

            bool isAdmin = idp.IsInClaim("Vrp_Tedport_Admin");


            response.isAdmin = isAdmin;

            return response;
        }

        [HttpGet]
        public GetSupplierUsersResponse GetSupplierUsers([FromUri]GetSupplierUsersRequest request)
        {
            var response = new GetSupplierUsersResponse();

            if (!HasClaims(response))
            {
                return response;
            }

            response = ProxyOf<ISupplierService>().GetSupplierUsers(request);
            return response;
        }

        [HttpGet]
        public GetSupplierClaimsResponse GetSupplierClaims()
        {
            var response = new GetSupplierClaimsResponse();

            if (!HasClaims(response))
            {
                return response;
            }

            response = ProxyOf<ISupplierService>().GetSupplierClaims();
            return response;
        }

        [HttpPut]
        public SaveSupplierUserResponse SaveSupplierUser([FromBody]SaveSupplierUserRequest request)
        {
            var response = new SaveSupplierUserResponse();

            bool isNewEntity = request.value.userRef == 0;

            if (!HasClaims(response))
            {
                return response;
            }

            response = ProxyOf<ISupplierService>().SaveSupplierUser(request);

            if (isNewEntity)
            {
                var idp = new FederationIdentityProvider();


                // appSettings item required or has default value?
                bool isHostedOnAzure = ConfigurationManager.AppSettings["IsHostedOnAzure"] == "true";
                bool mailSent = false;

                if (isHostedOnAzure)
                {
                    try
                    {
                        var queueEndpoint = ConfigurationManager.AppSettings["servicebusendpoint"];
                        var queueName = ConfigurationManager.AppSettings["SupplierQueueName"];

                        var user = new UserBasicInfo();
                        user.UserRef = response.value.userRef;
                        user.SupplierCode = idp.SupplierCode;
                        user.UserLogonRef = response.value.userLogonRef;
                        user.IsDomainLogon = false;
                        user.CanLogin = true;
                        user.MailAddress = response.value.mail;
                        user.UserName = response.value.mail;
                        user.CompanyRef = 1;
                        user.UserMailRef = response.value.userMailRef;
                        user.UserUid = response.value.userUid;
                        user.CreatedUserRef = response.value.createdUserRef;
                        user.CreatedDate = response.value.createdDate;
                        user.ModifiedUserRef = response.value.modifiedUserRef;
                        user.ModifiedDate = response.value.modifiedDate;

                        var mailMessage = new QueueMailMessage { MailAddress = request.value.mail, LanguageRef = 1 };

                        var supplier = new SupplierMessage();
                        supplier.MailMessage = mailMessage;
                        supplier.UserMessage = user;

                        ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Https;
                        QueueClient client = QueueClient.CreateFromConnectionString(queueEndpoint, queueName);
                        string msg = JsonConvert.SerializeObject(supplier);
                        client.Send(new BrokeredMessage(msg));
                        response.warningMessages = new string[] { msg };

                    }
                    catch (Exception ex)
                    {
                        response.warningMessages = new string[] { ex.Message + " " +  ex.InnerException?.Message};
                    }
                   
                }
                else
                {
                    mailSent = SendPasswordRequest(request.value.mail);
                }


            }

            return response;
        }

        [HttpPut]
        public SupplierUserExistsResponse SupplierUserExists([FromBody]SupplierUserExistsRequest request)
        {
            var response = new SupplierUserExistsResponse();

            if (!HasClaims(response))
            {
                return response;
            }

            response = ProxyOf<ISupplierService>().SupplierUserExists(request);
            return response;
        }

        [HttpPut]
        public ValidateUserResponse ValidateUser([FromBody]ValidateUserRequest request)
        {
            var response = new ValidateUserResponse();

            if (!HasClaims(response))
            {
                return response;
            }

            response = ProxyOf<ISupplierService>().ValidateUser(request);
            return response;
        }

        #region Util methods
        string PasswordResetLink
        {
            get
            {
                string ret = $"{ConfigurationManager.AppSettings["StsUrl"]}Account/ResetPasswordStepTwo?ResetPasswordRequestUid=";
                return ret;
            }
        }

        bool SendPasswordRequest(string mailAddress)
        {
            var serviceHelper = new ParsServiceHelper(new FederationIdentityProvider());
            var resetPasswordRequestUid = serviceHelper.CreatePasswordRequest(mailAddress);
            var mailTemplateResp = serviceHelper.GetMailTemplate(new MailTemplateGetRequest { MailTemplateKey = ConfigurationManager.AppSettings["PasswordResetTemplateKey"] });

            if (!mailTemplateResp.IsSucceeded)
            {
                return false;
            }

            var dict = new Dictionary<string, string>
            {
                {"#PASSWORDRESETLINK#", $"{this.PasswordResetLink}{resetPasswordRequestUid.ToString()}&he=true"}
            };

            var template = mailTemplateResp.MailTemplateList.FirstOrDefault();
            if (template == null)
            {
                return false;
            }
            foreach (var item in template.MailTemplateParameterRelation)
            {
                if (dict.ContainsKey(item.ParameterName))
                {
                    item.ParameterValue = dict[item.ParameterName];
                }
            }

            var valueRequest = new MailTemplateReplaceParametersWithValueRequest
            {
                MailTemplateParameterList = template.MailTemplateParameterRelation.ToArray(),
                MailTemplateRef = template.MailTemplateRef
            };

            var valueResponse = serviceHelper.GetMailTemplateWithReplacedValues(valueRequest);

            if (!valueResponse.IsSucceeded)
            {
                return false;
            }

            var req = new MailSendRequest()
            {
                Mail = new MailQueueDC()
                {
                    ApplicationRef = 1,
                    Body = valueResponse.MailTemplate.Body,
                    From = valueResponse.MailTemplate.MailFrom,
                    FromName = valueResponse.MailTemplate.MailFromName,
                    To = mailAddress,
                    Subject = valueResponse.MailTemplate.Subject,
                    Priority = 1, // sabit
                    Date = DateTime.Now // sabit
                }
            };

            serviceHelper.SendMail(req);
            return true;
        }

        bool HasClaims(ResponseBase response)
        {
            var idp = new FederationIdentityProvider();

            bool hasClaim = false;

            if (!idp.IsInClaim(FederationClaims.Vrp_TedPort_Admin) && !idp.IsInClaim(FederationClaims.Vrp_TedPort_Yonetici))
            {
                response.errorMessages = new[] { "You are not authorized to execute this service!" };
            }
            else
            {
                hasClaim = true;
            }

            return hasClaim;
        }
        #endregion

    }
}