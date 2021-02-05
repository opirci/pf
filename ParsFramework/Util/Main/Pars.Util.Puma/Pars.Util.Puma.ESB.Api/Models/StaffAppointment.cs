using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.ESB.Api.Models
{
    public class StaffAppointmentParent
    {
        public StaffAppointment StaffAppointment { get; set; }
    }
    public class StaffAppointment
    {
        public int HrPersonelRef { get; set; }
        public int AuthenticationTypeRef { get; set; }
    }
}