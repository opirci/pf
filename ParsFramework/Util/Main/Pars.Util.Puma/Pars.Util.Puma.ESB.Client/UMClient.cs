using Pars.Core.ESB.WebMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Pars.Util.Puma.ESB.Client
{
    public class UMClient
    {
        UMSubscriber<string> umSubscription;
        HttpClient client;
        string _postAddress;
        public UMClient(UMConfig config)
        {
            _postAddress = config.PostApiAddress;
            client = new HttpClient();
            umSubscription = UMSubscriber<string>.Subscribe(config.NspAddress, config.ChannelName, config.SubscriberName, PostMessage, -1);
        }

        private void PostMessage(string message)
        {
            var httpContent = new StringContent(message, Encoding.UTF8, "application/json");
            var response = client.PostAsync(_postAddress,httpContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                //TODO: log error message
            }

        }
    }
}