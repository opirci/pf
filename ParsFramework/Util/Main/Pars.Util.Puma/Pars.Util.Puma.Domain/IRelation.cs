using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public interface IRelation
    {
        int RelationRef { get; set; }
        int RelatedRef { get; set; }
        MemberState MemberState { get; set; }

        DateTime ValidFrom { get; set; }

        DateTime ValidTo { get; set; }
    }
}
