﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pars.Util.Puma.Service.IntegrationTest.SupplierSvc {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestBase", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierRequest))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredRequest))]
    public partial class RequestBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSupplierUsersRequest", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSupplierUsersRequest : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.RequestBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int partyRefField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int partyRef {
            get {
                return this.partyRefField;
            }
            set {
                if ((this.partyRefField.Equals(value) != true)) {
                    this.partyRefField = value;
                    this.RaisePropertyChanged("partyRef");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSupplierRequest", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSupplierRequest : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.RequestBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string supplierCodeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string supplierCode {
            get {
                return this.supplierCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.supplierCodeField, value) != true)) {
                    this.supplierCodeField = value;
                    this.RaisePropertyChanged("supplierCode");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSuppliersFilteredRequest", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSuppliersFilteredRequest : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.RequestBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] ageSexGroupsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] businessLinesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool hasNoUsersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int pageNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int pageSizeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int segmentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] subSegmentsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int supplierTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] ageSexGroups {
            get {
                return this.ageSexGroupsField;
            }
            set {
                if ((object.ReferenceEquals(this.ageSexGroupsField, value) != true)) {
                    this.ageSexGroupsField = value;
                    this.RaisePropertyChanged("ageSexGroups");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] businessLines {
            get {
                return this.businessLinesField;
            }
            set {
                if ((object.ReferenceEquals(this.businessLinesField, value) != true)) {
                    this.businessLinesField = value;
                    this.RaisePropertyChanged("businessLines");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code {
            get {
                return this.codeField;
            }
            set {
                if ((object.ReferenceEquals(this.codeField, value) != true)) {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool hasNoUsers {
            get {
                return this.hasNoUsersField;
            }
            set {
                if ((this.hasNoUsersField.Equals(value) != true)) {
                    this.hasNoUsersField = value;
                    this.RaisePropertyChanged("hasNoUsers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int pageNumber {
            get {
                return this.pageNumberField;
            }
            set {
                if ((this.pageNumberField.Equals(value) != true)) {
                    this.pageNumberField = value;
                    this.RaisePropertyChanged("pageNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int pageSize {
            get {
                return this.pageSizeField;
            }
            set {
                if ((this.pageSizeField.Equals(value) != true)) {
                    this.pageSizeField = value;
                    this.RaisePropertyChanged("pageSize");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int segment {
            get {
                return this.segmentField;
            }
            set {
                if ((this.segmentField.Equals(value) != true)) {
                    this.segmentField = value;
                    this.RaisePropertyChanged("segment");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] subSegments {
            get {
                return this.subSegmentsField;
            }
            set {
                if ((object.ReferenceEquals(this.subSegmentsField, value) != true)) {
                    this.subSegmentsField = value;
                    this.RaisePropertyChanged("subSegments");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int supplierType {
            get {
                return this.supplierTypeField;
            }
            set {
                if ((this.supplierTypeField.Equals(value) != true)) {
                    this.supplierTypeField = value;
                    this.RaisePropertyChanged("supplierType");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseBase", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierResponse))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredResponse))]
    public partial class ResponseBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] errorMessagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] warningMessagesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] errorMessages {
            get {
                return this.errorMessagesField;
            }
            set {
                if ((object.ReferenceEquals(this.errorMessagesField, value) != true)) {
                    this.errorMessagesField = value;
                    this.RaisePropertyChanged("errorMessages");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] warningMessages {
            get {
                return this.warningMessagesField;
            }
            set {
                if ((object.ReferenceEquals(this.warningMessagesField, value) != true)) {
                    this.warningMessagesField = value;
                    this.RaisePropertyChanged("warningMessages");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSupplierUsersResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSupplierUsersResponse : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ResponseBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.SupplierUser[] valuesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.SupplierUser[] values {
            get {
                return this.valuesField;
            }
            set {
                if ((object.ReferenceEquals(this.valuesField, value) != true)) {
                    this.valuesField = value;
                    this.RaisePropertyChanged("values");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSupplierResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSupplierResponse : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ResponseBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.Supplier valueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.Supplier value {
            get {
                return this.valueField;
            }
            set {
                if ((object.ReferenceEquals(this.valueField, value) != true)) {
                    this.valueField = value;
                    this.RaisePropertyChanged("value");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetSuppliersFilteredResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Supplier")]
    [System.SerializableAttribute()]
    public partial class GetSuppliersFilteredResponse : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ResponseBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int countField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.Supplier[] valuesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int count {
            get {
                return this.countField;
            }
            set {
                if ((this.countField.Equals(value) != true)) {
                    this.countField = value;
                    this.RaisePropertyChanged("count");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.Supplier[] values {
            get {
                return this.valuesField;
            }
            set {
                if ((object.ReferenceEquals(this.valuesField, value) != true)) {
                    this.valuesField = value;
                    this.RaisePropertyChanged("values");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Supplier", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    public partial class Supplier : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.DomainBaseOfint {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contactFirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contactLastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contactMailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int partyRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string supplierCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int supplierRefField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contactFirstName {
            get {
                return this.contactFirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.contactFirstNameField, value) != true)) {
                    this.contactFirstNameField = value;
                    this.RaisePropertyChanged("contactFirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contactLastName {
            get {
                return this.contactLastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.contactLastNameField, value) != true)) {
                    this.contactLastNameField = value;
                    this.RaisePropertyChanged("contactLastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contactMail {
            get {
                return this.contactMailField;
            }
            set {
                if ((object.ReferenceEquals(this.contactMailField, value) != true)) {
                    this.contactMailField = value;
                    this.RaisePropertyChanged("contactMail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int partyRef {
            get {
                return this.partyRefField;
            }
            set {
                if ((this.partyRefField.Equals(value) != true)) {
                    this.partyRefField = value;
                    this.RaisePropertyChanged("partyRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string supplierCode {
            get {
                return this.supplierCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.supplierCodeField, value) != true)) {
                    this.supplierCodeField = value;
                    this.RaisePropertyChanged("supplierCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int supplierRef {
            get {
                return this.supplierRefField;
            }
            set {
                if ((this.supplierRefField.Equals(value) != true)) {
                    this.supplierRefField = value;
                    this.RaisePropertyChanged("supplierRef");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SupplierUser", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    public partial class SupplierUser : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.DomainBaseOfint {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DomainBaseOfint", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.Supplier))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.SupplierUser))]
    public partial class DomainBaseOfint : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> HasChangedField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> HasChanged {
            get {
                return this.HasChangedField;
            }
            set {
                if ((this.HasChangedField.Equals(value) != true)) {
                    this.HasChangedField = value;
                    this.RaisePropertyChanged("HasChanged");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SupplierSvc.ISupplierService")]
    public interface ISupplierService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSuppliersFiltered", ReplyAction="http://tempuri.org/ISupplierService/GetSuppliersFilteredResponse")]
        Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredResponse GetSuppliersFiltered(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSuppliersFiltered", ReplyAction="http://tempuri.org/ISupplierService/GetSuppliersFilteredResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredResponse> GetSuppliersFilteredAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSupplierUsers", ReplyAction="http://tempuri.org/ISupplierService/GetSupplierUsersResponse")]
        Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersResponse GetSupplierUsers(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSupplierUsers", ReplyAction="http://tempuri.org/ISupplierService/GetSupplierUsersResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersResponse> GetSupplierUsersAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSupplier", ReplyAction="http://tempuri.org/ISupplierService/GetSupplierResponse")]
        Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierResponse GetSupplier(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupplierService/GetSupplier", ReplyAction="http://tempuri.org/ISupplierService/GetSupplierResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierResponse> GetSupplierAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISupplierServiceChannel : Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ISupplierService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SupplierServiceClient : System.ServiceModel.ClientBase<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ISupplierService>, Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.ISupplierService {
        
        public SupplierServiceClient() {
        }
        
        public SupplierServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SupplierServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupplierServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredResponse GetSuppliersFiltered(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredRequest request) {
            return base.Channel.GetSuppliersFiltered(request);
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredResponse> GetSuppliersFilteredAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSuppliersFilteredRequest request) {
            return base.Channel.GetSuppliersFilteredAsync(request);
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersResponse GetSupplierUsers(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersRequest request) {
            return base.Channel.GetSupplierUsers(request);
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersResponse> GetSupplierUsersAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierUsersRequest request) {
            return base.Channel.GetSupplierUsersAsync(request);
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierResponse GetSupplier(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierRequest request) {
            return base.Channel.GetSupplier(request);
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierResponse> GetSupplierAsync(Pars.Util.Puma.Service.IntegrationTest.SupplierSvc.GetSupplierRequest request) {
            return base.Channel.GetSupplierAsync(request);
        }
    }
}
