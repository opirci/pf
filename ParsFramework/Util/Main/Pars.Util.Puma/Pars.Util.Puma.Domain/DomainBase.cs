using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public abstract class DomainBase<T> : IDataChanged
    {
        //[DataMember]
        public abstract T Ref { get; }

        [DataMember]
        public bool? HasChanged { get; set; }

        private EntityStates _state;

        [DataMember]
        public EntityStates DataEntityState
        {
            get
            {
                if (IsStateDisabled || _state == EntityStates.Deleted)
                    return _state;

                var t = default(T);
                return t != null && t.Equals(Ref)
                    ? EntityStates.Added
                    : HasChanged.HasValue && HasChanged.Value ? EntityStates.Modified : EntityStates.Unchanged;
            }
            set { _state = value; }
        }

        public bool IsStateDisabled { get; set; }

        [OnSerializing]
        private void SetStates(StreamingContext context)
        {
            this.IsStateDisabled = true;
        }

        [OnSerialized]
        private void SetStatesBack(StreamingContext context)
        {
            this.IsStateDisabled = false;
        }

        //protected void SetProperty<T1>(ref T1 property, T1 value) //Moved to Extensions.cs
        //{
        //    if (value == null || value.Equals(property))
        //        return;

        //    property = value;
        //    HasChanged = true;
        //}
    }

    public interface IDataChanged
    {
        bool? HasChanged { get; set; }
    }
}
