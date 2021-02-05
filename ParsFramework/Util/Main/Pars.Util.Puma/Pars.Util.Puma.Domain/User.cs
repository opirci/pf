using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public class User : DomainBase<int>
    {
        public override int Ref => UserRef;
        public int UserRef { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
        public bool IsDomainUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int HrPersonelRef { get; set; }
    }
}
