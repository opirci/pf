using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class Range<T>
    {
        public static Range<T> From(T from) =>
            new Range<T>(from, default(T));

        public static Range<T> Until(T until) =>
            new Range<T>(default(T), until);

        public static Range<T> Between(T from, T until) =>
            new Range<T>(from, until);

        public bool IsFrom => Start != null && End == null;
        public bool IsUntil => Start == null && End != null;
        public bool IsBetween => Start != null && End != null;
        public bool IsNoRange => Start == null && End == null;

        [DataMember(Name = "start")]
        public T Start { get; set; }
        [DataMember(Name = "end")]
        public T End { get; set; }

        public Range(T start, T end)
        {
            Start = start;
            End = end;
        }

        public Range() : this(default(T), default(T))
        {
        }
    }

}
