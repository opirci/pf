using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class DateRange : Range<DateTime>
    {
        //public DateRange(DateTime start, DateTime end) : base(start, end)
        //{
        //}
    }

    //[DataContract]
    //public struct DateRange
    //{
    //    public static DateRange From(DateTime from) =>
    //        new DateRange(from, null);

    //    public static DateRange Until(DateTime until) =>
    //        new DateRange(null, until);

    //    public static DateRange Between(DateTime from, DateTime until) =>
    //        new DateRange(from, until);


    //    public bool IsFrom => Start != null && End == null;
    //    public bool IsUntil => Start == null && End != null;
    //    public bool IsBetween => Start != null && End != null;
    //    public bool IsNoRange => Start == null && End == null;

    //    [DataMember(Name = "start")]
    //    public DateTime? Start { get; set; }
    //    [DataMember(Name = "end")]
    //    public DateTime? End { get; set; }

    //    public DateRange(DateTime? start, DateTime? end)
    //    {
    //        Start = start;
    //        End = end;
    //    }
    //}

}
