using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class RoleRelation : Role, IRelation// IMapRelation<int>//IMapRelation<int, int>
    {
        private int _relationRef;
        private int _relatedRef;
        private DateTime _validFrom;
        public DateTime _validTo;
        private MemberState _memberState;

        public RoleRelation()
        {

        }

        public RoleRelation(Role role, int relatedRef, int relationRef)
        {
            this.RelatedRef = relatedRef;
            this.RelationRef = relationRef;
            this.Name = role.Name;
            this.RoleRef = role.RoleRef;
            this.LocaleData = new LocaleData
            {
                LocaleRef = role.LocaleData.LocaleRef,
                Value = role.LocaleData.Value
            };
            this.EntityState = new EntityState
            {
                EntityStateRef = role.EntityState.EntityStateRef,
                Name = role.EntityState.Name,
                Description = role.EntityState.Description
            };

        }

        public static RoleRelation Create()
        {
            return new RoleRelation();
        }

        [DataMember(Name="relationRef")]
        public int RelationRef
        {
            get { return _relationRef; }
            set { this.SetProperty(ref _relationRef, value); }
        }

        [DataMember(Name = "RelatedRef")]
        public int RelatedRef
        {
            get { return _relatedRef; }
            set { this.SetProperty(ref _relatedRef, value); }
        }

        [DataMember(Name = "validFrom")]
        public DateTime ValidFrom
        {
            get { return _validFrom; }
            set { this.SetProperty(ref _validFrom, value); }
        }

        [DataMember(Name = "validTo")]
        public DateTime ValidTo
        {
            get { return _validTo; }
            set { this.SetProperty(ref _validTo, value); }
        }

        [DataMember(Name = "memberState")]
        public MemberState MemberState
        {
            get { return _memberState; }
            set { this.SetProperty(ref _memberState, value); }
        }
    }
}
