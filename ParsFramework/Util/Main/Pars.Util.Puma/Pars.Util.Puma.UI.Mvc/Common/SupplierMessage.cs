using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.UI.Mvc.Common
{
    public class SupplierMessage
    {
        public QueueMailMessage MailMessage { get; set; }

        public UserBasicInfo UserMessage { get; set; }
    }
}