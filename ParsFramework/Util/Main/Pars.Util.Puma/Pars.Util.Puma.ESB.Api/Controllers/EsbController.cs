using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Pars.Common.Logging.Client;
using Pars.Core.Security;
using Pars.Util.Puma.ESB.Api.Models;
using Pars.Util.Puma.ESB.Api.UserServicePublic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Pars.Util.Puma.ESB.Api.Controllers
{
    public class EsbController : ApiController
    {
        public static IIdentityProvider identityProvider;
        private static ILoggingProvider _loggingProvider;
        static EsbController()
        {
            _loggingProvider = new ParsLoggingProvider(new Logger());
            identityProvider = new FederationIdentityProvider();
            SetIIdentityProvider();
        }

        public EsbController()
        {
            _loggingProvider = new ParsLoggingProvider(new Logger());
        }

        [HttpPost]
        [Route("api/esb/savesupplier")]
        public void SaveSupplier([FromBody]SupplierObject supplier)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string mail = "";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"]))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CreateSupplier", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@SupplierCode", SqlDbType.VarChar).Value = supplier.Supplier.SupplierCode;
                        cmd.Parameters.Add("@SupplierName", SqlDbType.VarChar).Value = supplier.Supplier.SupplierName;
                        cmd.Parameters.Add("@CountryISOCode", SqlDbType.VarChar).Value = supplier.Supplier.CountryISOCode;
                        cmd.Parameters.Add("@SupplierType", SqlDbType.VarChar).Value = supplier.Supplier.SupplierType;
                        cmd.Parameters.Add("@Segment", SqlDbType.VarChar).Value = supplier.Supplier.Segment;
                        cmd.Parameters.Add("@ContactFirstName", SqlDbType.VarChar).Value = supplier.Supplier.ContactFirstName;
                        cmd.Parameters.Add("@ContactLastName", SqlDbType.VarChar).Value = supplier.Supplier.ContactLastName;
                        cmd.Parameters.Add("@ContactEmail", SqlDbType.VarChar).Value = supplier.Supplier.ContactEmail;

                        con.Open();

                        _loggingProvider.WriteInfo(new LogEntry
                        {
                            AccountName = identityProvider.UserName,
                            CommandName = "SaveSupplier",
                            Message = "conn opened"
                        });

                        cmd.ExecuteNonQuery();
                        con.Close();

                        var queueEndpoint = ConfigurationManager.AppSettings["servicebusendpoint"];
                        var queueName = ConfigurationManager.AppSettings["SupplierQueueName"];

                        var mailMessage = new QueueMailMessage
                        {
                            MailAddress = supplier.Supplier.ContactEmail,
                            LanguageRef = supplier.Supplier.CountryISOCode == "TUR" ? 1 : 2
                        };

                        ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Https;
                        QueueClient client = QueueClient.CreateFromConnectionString(queueEndpoint, queueName);
                        string msg = JsonConvert.SerializeObject(mailMessage);
                        client.Send(new BrokeredMessage(msg));

                        _loggingProvider.WriteInfo(new LogEntry
                        {
                            AccountName = identityProvider.UserName,
                            CommandName =
                        $"Supplier Saved: {supplier.Supplier.SupplierCode} - {supplier.Supplier.SupplierName}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "SaveSupplier",
                    Exception = ex,
                });
            }
        }

        [HttpPost]
        [Route("api/esb/createnewuser")]
        public void CreateNewUser([FromBody]HarmonyUserParent user)
        {
            try
            {
                //gelen user form user değilse configdeki değeri oku ona göre karar ver, form user ise direk oluştur
                if (user.HarmonyUser.AuthenticationType == (int)Pars.Util.Puma.ESB.Api.Models.AuthenticationType.FORM || !Boolean.Parse(ConfigurationManager.AppSettings["CreateOnlyFormUsers"]))
                {
                    CheckToken();
                    Pars.Core.Service.ProxyBase<UserServicePublic.IUserServicePublic> userService = new Core.Service.ProxyBase<UserServicePublic.IUserServicePublic>(identityProvider, "WS2007FederationHttpBinding_IUserServicePublic");
                    var userFromDB = userService.Client.GetUserByHrPersonelRef(user.HarmonyUser.HrPersonelRef);
                    if (userFromDB == null)
                    {
                        PhoneNumber phone = FormatPhoneNumber(user.HarmonyUser.PhoneNumber);
                        _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "CreateNewUser", Message = "CreateNewUser started. UserType : " + user.HarmonyUser.AuthenticationType.ToString() });
                        UserServicePublic.NewUserRequest newUser = new UserServicePublic.NewUserRequest
                        {
                            UserType = user.HarmonyUser.AuthenticationType == (int)Pars.Util.Puma.ESB.Api.Models.AuthenticationType.DOMAIN ? UserType.Domain : UserType.Other,
                            CreateLdapUser = user.HarmonyUser.AuthenticationType == (int)Pars.Util.Puma.ESB.Api.Models.AuthenticationType.DOMAIN ? true : false,
                            DomainName = user.HarmonyUser.AuthenticationType == (int)Pars.Util.Puma.ESB.Api.Models.AuthenticationType.DOMAIN ? "LCWAIKIKI" : string.Empty,
                            HRPersonelRef = user.HarmonyUser.HrPersonelRef,
                            UserFirstName = user.HarmonyUser.FirstName,
                            UserLastName = user.HarmonyUser.LastName,
                            UserEmail = user.HarmonyUser.Email,
                            CompanyRef = 1,
                            SuggestedUsername = user.HarmonyUser.SuggestedUsername,
                            MobileAreaCode = phone.MobileAreaCode,
                            PhoneNumber = phone.Mobile,
                            BirthDate = user.HarmonyUser.BirthDate
                        };


                        userService.Client.CreateNewUser(newUser);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "CreateNewUser",
                    Exception = ex,
                    Message = String.Format("User : {0} , Message :{1} - Stack Trace : {2}", user.HarmonyUser.ToString(), ex.Message, ex.StackTrace)
                });
            }
        }

        [HttpPost]
        [Route("api/esb/updateuserinfo")]
        public void UpdateUserInfo([FromBody] HarmonyUserParent user)
        {
            try
            {
                CheckToken();
                Pars.Core.Service.ProxyBase<UserServicePublic.IUserServicePublic> userService = new Core.Service.ProxyBase<UserServicePublic.IUserServicePublic>(identityProvider, "WS2007FederationHttpBinding_IUserServicePublic");
                if (!string.IsNullOrEmpty(user.HarmonyUser.PhoneNumber))
                {
                    PhoneNumber phone = FormatPhoneNumber(user.HarmonyUser.PhoneNumber);

                    userService.Client.UpdateUserInfo(user.HarmonyUser.HrPersonelRef, phone.MobileAreaCode,
                        phone.Mobile);
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "UpdateUserInfo", Message = user.HarmonyUser.PhoneNumber });
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "UpdateUserInfo",
                    Exception = ex,
                    Message = String.Format("User : {0} , Message :{1} - Stack Trace : {2}", user.HarmonyUser.ToString(), ex.Message, ex.StackTrace)
                });
            }
        }

        [HttpPost]
        [Route("api/esb/recreateuser")]
        public void RecreateUser([FromBody]HarmonyUserParent user)
        {
            try
            {
                Pars.Core.Service.ProxyBase<UserServicePublic.IUserServicePublic> userService = new Core.Service.ProxyBase<UserServicePublic.IUserServicePublic>();
                var userFromDB = userService.Client.GetUserByHrPersonelRef(user.HarmonyUser.HrPersonelRef);
                if (userFromDB != null && userFromDB.IsActive == false)
                {
                    userService.Client.UpdateUser(user.HarmonyUser.HrPersonelRef, user.HarmonyUser.FirstName, user.HarmonyUser.LastName, true);
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "RecreateUser",
                    Exception = ex,
                    Message = String.Format("User : {0} , Message :{1} - Stack Trace : {2}", user.HarmonyUser.HrPersonelRef.ToString(), ex.Message, ex.StackTrace)
                });
            }
        }

        [HttpPost]
        [Route("api/esb/staffappointment")]
        public void StaffAppointment([FromBody]StaffAppointmentParent hrUser)
        {
            try
            {
                if (hrUser.StaffAppointment.AuthenticationTypeRef == (int)Pars.Util.Puma.ESB.Api.Models.AuthenticationType.DOMAIN && !Boolean.Parse(ConfigurationManager.AppSettings["CreateOnlyFormUsers"]))
                {
                    _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "StaffAppointment", Message = "StaffAppointment started" });
                    CheckToken();
                    Pars.Core.Service.ProxyBase<UserServicePublic.IUserServicePublic> userService = new Core.Service.ProxyBase<UserServicePublic.IUserServicePublic>(identityProvider, "WS2007FederationHttpBinding_IUserServicePublic");

                    UserBase user = userService.Client.GetUserByHrPersonelRef(hrUser.StaffAppointment.HrPersonelRef);

                    if (user != null && !user.IsDomainUser)
                    {
                        userService.Client.UpdateUser(user.UserRef, string.Empty, string.Empty, false);
                        UserServicePublic.NewUserRequest newUser = new UserServicePublic.NewUserRequest
                        {
                            UserType = UserType.Domain,
                            CreateLdapUser = true,
                            DomainName = "LCWAIKIKI",
                            HRPersonelRef = hrUser.StaffAppointment.HrPersonelRef,
                            UserFirstName = user.FirstName,
                            UserLastName = user.SurName,
                            UserEmail = user.MailAddress,
                            CompanyRef = 1,
                            SuggestedUsername = string.Empty,
                            MobileAreaCode = user.MobileAreaCode,
                            PhoneNumber = user.Mobile
                        };
                        userService.Client.CreateNewUser(newUser);
                    }
                    else
                    {
                        _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "StaffAppointment", Message = "Can not find active user." });
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "StaffAppointment",
                    Exception = ex,
                    Message = String.Format("AuthenticationTypeRef  : {0} ,HrPersonelRef : {1} , Message :{2} - Stack Trace : {3}", hrUser.StaffAppointment.AuthenticationTypeRef.ToString(), hrUser.StaffAppointment.HrPersonelRef.ToString(), ex.Message, ex.StackTrace)
                });
            }
        }

        [HttpPost]
        [Route("api/esb/resininguser")]
        public void ResiningUser([FromBody]HarmonyUserParent hrUser)
        {
            try
            {
                //_loggingProvider = new ParsLoggingProvider(new Logger());
                // _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "ResiningUser", Message = "ResiningUser started" });
                CheckToken();
                Pars.Core.Service.ProxyBase<UserServicePublic.IUserServicePublic> userService = new Core.Service.ProxyBase<UserServicePublic.IUserServicePublic>(identityProvider, "WS2007FederationHttpBinding_IUserServicePublic");
                if (hrUser.HarmonyUser.HrPersonelRef > 0)
                {
                    UserBase user = userService.Client.GetUserByHrPersonelRef(hrUser.HarmonyUser.HrPersonelRef);
                    if (user != null)
                    {
                        userService.Client.UpdateUser(user.UserRef, string.Empty, string.Empty, false);
                    }
                    else
                    {
                        _loggingProvider.WriteInfo(new LogEntry { AccountName = identityProvider.UserName, CommandName = "ResiningUser", Message = "Can not find active user." });
                    }
                }
                else
                {
                    throw new ArgumentNullException("hrUser.HarmonyUser.HrPersonelRef", "HrPersonelRef is null");
                }
            }
            catch (Exception ex)
            {
                _loggingProvider.WriteError(new LogEntry
                {
                    AccountName = identityProvider.UserName,
                    CommandName = "ResiningUser",
                    Exception = ex,
                    Message = String.Format("User : {0} , Message :{1} - Stack Trace : {2}", hrUser.HarmonyUser.ToString(), ex.Message, ex.StackTrace)
                });
            }
        }

        private void CheckToken()
        {
            if (identityProvider == null || identityProvider.UserName == null || identityProvider.Token.ValidTo.ToLocalTime() < DateTime.Now)
            {
                SetIIdentityProvider();
            }
        }

        private static void SetIIdentityProvider()
        {
            try
            {
                string environmentKey = ConfigurationManager.AppSettings["Environment"];//test : live : dev

                string relayingParty = "http://localhost/wpfshell/";
                string issuerAddress = "https://sts" + environmentKey + ".lcwaikiki.com/sts/issue/wstrust/mixed/username";
                string issuerName = "https://sts" + environmentKey + ".lcwaikiki.com";
                string spnIdentity = "ESBSERVICEUSER";


                ClaimsPrincipal claimsPrincipal = null;

                var fim = new Pars.Core.IdentityModel.FederationIdentityManager();
                claimsPrincipal = fim.GetPrincipal(1, relayingParty, issuerAddress, issuerName, spnIdentity, "3sbUSR987");

                System.Threading.Thread.CurrentPrincipal = claimsPrincipal;
            }
            catch (Exception ex)
            {

                _loggingProvider.WriteError(new LogEntry { AccountName = identityProvider.UserName, CommandName = "SetIIdentityProvider", Message = string.Format("Environment : {2} Message :{0} - Stack Trace : {1}", ex.Message, ex.StackTrace, ConfigurationManager.AppSettings["Environment"]) });
            }
            //System.AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
        }

        private struct PhoneNumber
        {
            public string MobileAreaCode;
            public string Mobile;
        }

        private PhoneNumber FormatPhoneNumber(string phoneNumber)
        {
            PhoneNumber result = new PhoneNumber();
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                string temp = phoneNumber.TrimStart('0');
                temp = Regex.Replace(temp, @"\s+", "");

                result.MobileAreaCode = temp.Substring(0, temp.IndexOf('('));
                result.Mobile = temp.Substring(temp.IndexOf('(') + 1).Replace(")", string.Empty);
            }
            return result;
        }
    }


}