﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetEntityStatesResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Definition")]
    [System.SerializableAttribute()]
    public partial class GetEntityStatesResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.EntityState> EntityStatesField;
        
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
        public System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.EntityState> EntityStates {
            get {
                return this.EntityStatesField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityStatesField, value) != true)) {
                    this.EntityStatesField = value;
                    this.RaisePropertyChanged("EntityStates");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityState", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    public partial class EntityState : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private short EntityStateRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
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
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public short EntityStateRef {
            get {
                return this.EntityStateRefField;
            }
            set {
                if ((this.EntityStateRefField.Equals(value) != true)) {
                    this.EntityStateRefField = value;
                    this.RaisePropertyChanged("EntityStateRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MemberStateListResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service.Definition")]
    [System.SerializableAttribute()]
    public partial class MemberStateListResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberState> MemberStatesField;
        
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
        public System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberState> MemberStates {
            get {
                return this.MemberStatesField;
            }
            set {
                if ((object.ReferenceEquals(this.MemberStatesField, value) != true)) {
                    this.MemberStatesField = value;
                    this.RaisePropertyChanged("MemberStates");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MemberState", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    public partial class MemberState : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private short MemberStateRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
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
        public short MemberStateRef {
            get {
                return this.MemberStateRefField;
            }
            set {
                if ((this.MemberStateRefField.Equals(value) != true)) {
                    this.MemberStateRefField = value;
                    this.RaisePropertyChanged("MemberStateRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseBase", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse))]
    public partial class ResponseBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<string> errorMessagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<string> warningMessagesField;
        
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
        public System.Collections.Generic.List<string> errorMessages {
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
        public System.Collections.Generic.List<string> warningMessages {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="LookupResponse", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Service")]
    [System.SerializableAttribute()]
    public partial class LookupResponse : Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.ResponseBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupItem> valuesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupItem> values {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="LookupItem", Namespace="http://schemas.datacontract.org/2004/07/Pars.Util.Puma.Domain")]
    [System.SerializableAttribute()]
    public partial class LookupItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RefField;
        
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
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ref {
            get {
                return this.RefField;
            }
            set {
                if ((this.RefField.Equals(value) != true)) {
                    this.RefField = value;
                    this.RaisePropertyChanged("Ref");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DefinitionSvc.IDefinitionService")]
    public interface IDefinitionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetEntityStates", ReplyAction="http://tempuri.org/IDefinitionService/GetEntityStatesResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.GetEntityStatesResponse GetEntityStates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetEntityStates", ReplyAction="http://tempuri.org/IDefinitionService/GetEntityStatesResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.GetEntityStatesResponse> GetEntityStatesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetMemberStates", ReplyAction="http://tempuri.org/IDefinitionService/GetMemberStatesResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberStateListResponse GetMemberStates();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetMemberStates", ReplyAction="http://tempuri.org/IDefinitionService/GetMemberStatesResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberStateListResponse> GetMemberStatesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSegmentsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSegmentsAsLookupResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSegmentsAsLookup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSegmentsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSegmentsAsLookupResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSegmentsAsLookupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSubSegmentsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSubSegmentsAsLookupResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSubSegmentsAsLookup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSubSegmentsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSubSegmentsAsLookupResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSubSegmentsAsLookupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetBusinessLinesAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetBusinessLinesAsLookupResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetBusinessLinesAsLookup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetBusinessLinesAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetBusinessLinesAsLookupResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetBusinessLinesAsLookupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetAgeSexGroupsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetAgeSexGroupsAsLookupResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetAgeSexGroupsAsLookup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetAgeSexGroupsAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetAgeSexGroupsAsLookupResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetAgeSexGroupsAsLookupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSupplierTypesAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSupplierTypesAsLookupResponse")]
        Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSupplierTypesAsLookup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDefinitionService/GetSupplierTypesAsLookup", ReplyAction="http://tempuri.org/IDefinitionService/GetSupplierTypesAsLookupResponse")]
        System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSupplierTypesAsLookupAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDefinitionServiceChannel : Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.IDefinitionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DefinitionServiceClient : System.ServiceModel.ClientBase<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.IDefinitionService>, Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.IDefinitionService {
        
        public DefinitionServiceClient() {
        }
        
        public DefinitionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DefinitionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DefinitionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DefinitionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.GetEntityStatesResponse GetEntityStates() {
            return base.Channel.GetEntityStates();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.GetEntityStatesResponse> GetEntityStatesAsync() {
            return base.Channel.GetEntityStatesAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberStateListResponse GetMemberStates() {
            return base.Channel.GetMemberStates();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.MemberStateListResponse> GetMemberStatesAsync() {
            return base.Channel.GetMemberStatesAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSegmentsAsLookup() {
            return base.Channel.GetSegmentsAsLookup();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSegmentsAsLookupAsync() {
            return base.Channel.GetSegmentsAsLookupAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSubSegmentsAsLookup() {
            return base.Channel.GetSubSegmentsAsLookup();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSubSegmentsAsLookupAsync() {
            return base.Channel.GetSubSegmentsAsLookupAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetBusinessLinesAsLookup() {
            return base.Channel.GetBusinessLinesAsLookup();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetBusinessLinesAsLookupAsync() {
            return base.Channel.GetBusinessLinesAsLookupAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetAgeSexGroupsAsLookup() {
            return base.Channel.GetAgeSexGroupsAsLookup();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetAgeSexGroupsAsLookupAsync() {
            return base.Channel.GetAgeSexGroupsAsLookupAsync();
        }
        
        public Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse GetSupplierTypesAsLookup() {
            return base.Channel.GetSupplierTypesAsLookup();
        }
        
        public System.Threading.Tasks.Task<Pars.Util.Puma.Service.IntegrationTest.DefinitionSvc.LookupResponse> GetSupplierTypesAsLookupAsync() {
            return base.Channel.GetSupplierTypesAsLookupAsync();
        }
    }
}
