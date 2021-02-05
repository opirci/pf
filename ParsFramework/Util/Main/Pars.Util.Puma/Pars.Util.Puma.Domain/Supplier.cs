using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class Supplier : DomainBase<int>
    {

        int _supplierRef;

        [DataMember(Name = "supplierRef")]
        public int SupplierRef
        {
            get { return _supplierRef; }
            set { this.SetProperty(ref _supplierRef, value); }
        }

        int _partyRef;

        [DataMember(Name = "partyRef")]
        public int PartyRef
        {
            get { return _partyRef; }
            set { this.SetProperty(ref _partyRef, value); }
        }

        string _supplierCode;

        [DataMember(Name = "supplierCode")]
        public string SupplierCode
        {
            get { return _supplierCode; }
            set { this.SetProperty(ref _supplierCode, value); }
        }


        string _name;

        [DataMember(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value); }
        }

        private string _contactFirstName;
        [DataMember(Name = "contactFirstName")]

        public string ContactFirstName
        {
            get { return _contactFirstName; }
            set { this.SetProperty(ref _contactFirstName, value); }
        }

        private string _contactLastName;
        [DataMember(Name = "contactLastName")]

        public string ContactLastName
        {
            get { return _contactLastName; }
            set { this.SetProperty(ref _contactLastName, value); }
        }

        private string _ContactMail;

        [DataMember(Name = "contactMail")]
        public string ContactMail
        {
            get { return _ContactMail; }
            set { this.SetProperty(ref _ContactMail, value); }
        }

        private int _countryRef;

        [DataMember(Name = "countryRef")]
        public int CountryRef
        {
            get
            {
                return _countryRef;
            }
            set
            {
                _countryRef = value;
            }
        }


        //int _partyRef;

        //[DataMember(Name = "partyRef")]
        //public int PartyRef
        //{
        //    get { return _partyRef; }
        //    set { this.SetProperty(ref _partyRef, value); }
        //}


        //int _countryRef;

        //[DataMember(Name = "countryRef")]
        //public int CountryRef
        //{
        //    get { return _countryRef; }
        //    set { this.SetProperty(ref _countryRef, value); }
        //}


        //int _segmentRef;

        //[DataMember(Name = "segmentRef")]
        //public int SegmentRef
        //{
        //    get { return _segmentRef; }
        //    set { this.SetProperty(ref _segmentRef, value); }
        //}


        //int _supplierTypeRef;

        //[DataMember(Name = "supplierTypeRef")]
        //public int SupplierTypeRef
        //{
        //    get { return _supplierTypeRef; }
        //    set { this.SetProperty(ref _supplierTypeRef, value); }
        //}

        public override int Ref => SupplierRef;
    }
}
