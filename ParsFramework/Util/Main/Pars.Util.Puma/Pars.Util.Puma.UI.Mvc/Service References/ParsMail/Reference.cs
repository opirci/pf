﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pars.Util.Puma.UI.Mvc.ParsMail {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailSendRequest", Namespace="http://schemas.datacontract.org/2004/07/Pars.Common.EmailService.Shared")]
    [System.SerializableAttribute()]
    public partial class MailSendRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueDC MailField;
        
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
        public Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueDC Mail {
            get {
                return this.MailField;
            }
            set {
                if ((object.ReferenceEquals(this.MailField, value) != true)) {
                    this.MailField = value;
                    this.RaisePropertyChanged("Mail");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MailQueueDC", Namespace="http://schemas.datacontract.org/2004/07/Pars.Common.EmailService.Shared")]
    [System.SerializableAttribute()]
    public partial class MailQueueDC : Pars.Core.Data.EntityBase, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MailQueueRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ApplicationRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BccField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CcField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SubjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> LastRetryDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RetryCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StatusRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] RowVersionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CreatedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ModifiedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> SessionRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueAttachmentDC[] MailQueueAttachmentField;
        
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
        public int MailQueueRef {
            get {
                return this.MailQueueRefField;
            }
            set {
                if ((this.MailQueueRefField.Equals(value) != true)) {
                    this.MailQueueRefField = value;
                    this.RaisePropertyChanged("MailQueueRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int ApplicationRef {
            get {
                return this.ApplicationRefField;
            }
            set {
                if ((this.ApplicationRefField.Equals(value) != true)) {
                    this.ApplicationRefField = value;
                    this.RaisePropertyChanged("ApplicationRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string From {
            get {
                return this.FromField;
            }
            set {
                if ((object.ReferenceEquals(this.FromField, value) != true)) {
                    this.FromField = value;
                    this.RaisePropertyChanged("From");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string FromName {
            get {
                return this.FromNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FromNameField, value) != true)) {
                    this.FromNameField = value;
                    this.RaisePropertyChanged("FromName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string To {
            get {
                return this.ToField;
            }
            set {
                if ((object.ReferenceEquals(this.ToField, value) != true)) {
                    this.ToField = value;
                    this.RaisePropertyChanged("To");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string Bcc {
            get {
                return this.BccField;
            }
            set {
                if ((object.ReferenceEquals(this.BccField, value) != true)) {
                    this.BccField = value;
                    this.RaisePropertyChanged("Bcc");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string Cc {
            get {
                return this.CcField;
            }
            set {
                if ((object.ReferenceEquals(this.CcField, value) != true)) {
                    this.CcField = value;
                    this.RaisePropertyChanged("Cc");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public string Subject {
            get {
                return this.SubjectField;
            }
            set {
                if ((object.ReferenceEquals(this.SubjectField, value) != true)) {
                    this.SubjectField = value;
                    this.RaisePropertyChanged("Subject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public string Body {
            get {
                return this.BodyField;
            }
            set {
                if ((object.ReferenceEquals(this.BodyField, value) != true)) {
                    this.BodyField = value;
                    this.RaisePropertyChanged("Body");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public System.Nullable<System.DateTime> LastRetryDate {
            get {
                return this.LastRetryDateField;
            }
            set {
                if ((this.LastRetryDateField.Equals(value) != true)) {
                    this.LastRetryDateField = value;
                    this.RaisePropertyChanged("LastRetryDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public int Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public System.Nullable<int> RetryCount {
            get {
                return this.RetryCountField;
            }
            set {
                if ((this.RetryCountField.Equals(value) != true)) {
                    this.RetryCountField = value;
                    this.RaisePropertyChanged("RetryCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=13)]
        public int StatusRef {
            get {
                return this.StatusRefField;
            }
            set {
                if ((this.StatusRefField.Equals(value) != true)) {
                    this.StatusRefField = value;
                    this.RaisePropertyChanged("StatusRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public byte[] RowVersion {
            get {
                return this.RowVersionField;
            }
            set {
                if ((object.ReferenceEquals(this.RowVersionField, value) != true)) {
                    this.RowVersionField = value;
                    this.RaisePropertyChanged("RowVersion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=15)]
        public int CreatedUserRef {
            get {
                return this.CreatedUserRefField;
            }
            set {
                if ((this.CreatedUserRefField.Equals(value) != true)) {
                    this.CreatedUserRefField = value;
                    this.RaisePropertyChanged("CreatedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=16)]
        public System.DateTime CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=17)]
        public int ModifiedUserRef {
            get {
                return this.ModifiedUserRefField;
            }
            set {
                if ((this.ModifiedUserRefField.Equals(value) != true)) {
                    this.ModifiedUserRefField = value;
                    this.RaisePropertyChanged("ModifiedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=18)]
        public System.DateTime ModifiedDate {
            get {
                return this.ModifiedDateField;
            }
            set {
                if ((this.ModifiedDateField.Equals(value) != true)) {
                    this.ModifiedDateField = value;
                    this.RaisePropertyChanged("ModifiedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=19)]
        public System.Nullable<System.Guid> SessionRef {
            get {
                return this.SessionRefField;
            }
            set {
                if ((this.SessionRefField.Equals(value) != true)) {
                    this.SessionRefField = value;
                    this.RaisePropertyChanged("SessionRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=20)]
        public Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueAttachmentDC[] MailQueueAttachment {
            get {
                return this.MailQueueAttachmentField;
            }
            set {
                if ((object.ReferenceEquals(this.MailQueueAttachmentField, value) != true)) {
                    this.MailQueueAttachmentField = value;
                    this.RaisePropertyChanged("MailQueueAttachment");
                }
            }
        }

        public override int ObjectRef
        {
            get { return 0; }
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MailQueueAttachmentDC", Namespace="http://schemas.datacontract.org/2004/07/Pars.Common.EmailService.Shared")]
    [System.SerializableAttribute()]
    public partial class MailQueueAttachmentDC : Pars.Core.Data.EntityBase, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MailQueueAttachmentRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MailQueueRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> AttachmentRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] RowVersionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CreatedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ModifiedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> SessionRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.UI.Mvc.ParsMail.MailAttachmentDC MailAttachmentField;
        
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
        public int MailQueueAttachmentRef {
            get {
                return this.MailQueueAttachmentRefField;
            }
            set {
                if ((this.MailQueueAttachmentRefField.Equals(value) != true)) {
                    this.MailQueueAttachmentRefField = value;
                    this.RaisePropertyChanged("MailQueueAttachmentRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MailQueueRef {
            get {
                return this.MailQueueRefField;
            }
            set {
                if ((this.MailQueueRefField.Equals(value) != true)) {
                    this.MailQueueRefField = value;
                    this.RaisePropertyChanged("MailQueueRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.Nullable<int> AttachmentRef {
            get {
                return this.AttachmentRefField;
            }
            set {
                if ((this.AttachmentRefField.Equals(value) != true)) {
                    this.AttachmentRefField = value;
                    this.RaisePropertyChanged("AttachmentRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte[] RowVersion {
            get {
                return this.RowVersionField;
            }
            set {
                if ((object.ReferenceEquals(this.RowVersionField, value) != true)) {
                    this.RowVersionField = value;
                    this.RaisePropertyChanged("RowVersion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int CreatedUserRef {
            get {
                return this.CreatedUserRefField;
            }
            set {
                if ((this.CreatedUserRefField.Equals(value) != true)) {
                    this.CreatedUserRefField = value;
                    this.RaisePropertyChanged("CreatedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public System.DateTime CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public int ModifiedUserRef {
            get {
                return this.ModifiedUserRefField;
            }
            set {
                if ((this.ModifiedUserRefField.Equals(value) != true)) {
                    this.ModifiedUserRefField = value;
                    this.RaisePropertyChanged("ModifiedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public System.DateTime ModifiedDate {
            get {
                return this.ModifiedDateField;
            }
            set {
                if ((this.ModifiedDateField.Equals(value) != true)) {
                    this.ModifiedDateField = value;
                    this.RaisePropertyChanged("ModifiedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public System.Nullable<System.Guid> SessionRef {
            get {
                return this.SessionRefField;
            }
            set {
                if ((this.SessionRefField.Equals(value) != true)) {
                    this.SessionRefField = value;
                    this.RaisePropertyChanged("SessionRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public Pars.Util.Puma.UI.Mvc.ParsMail.MailAttachmentDC MailAttachment {
            get {
                return this.MailAttachmentField;
            }
            set {
                if ((object.ReferenceEquals(this.MailAttachmentField, value) != true)) {
                    this.MailAttachmentField = value;
                    this.RaisePropertyChanged("MailAttachment");
                }
            }
        }

        public override int ObjectRef
        {
            get { return 0; }
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MailAttachmentDC", Namespace="http://schemas.datacontract.org/2004/07/Pars.Common.EmailService.Shared")]
    [System.SerializableAttribute()]
    public partial class MailAttachmentDC : Pars.Core.Data.EntityBase, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AttachmentRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] RowVersionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CreatedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ModifiedUserRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> SessionRefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueAttachmentDC[] MailQueueAttachmentField;
        
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
        public int AttachmentRef {
            get {
                return this.AttachmentRefField;
            }
            set {
                if ((this.AttachmentRefField.Equals(value) != true)) {
                    this.AttachmentRefField = value;
                    this.RaisePropertyChanged("AttachmentRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public byte[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte[] RowVersion {
            get {
                return this.RowVersionField;
            }
            set {
                if ((object.ReferenceEquals(this.RowVersionField, value) != true)) {
                    this.RowVersionField = value;
                    this.RaisePropertyChanged("RowVersion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int CreatedUserRef {
            get {
                return this.CreatedUserRefField;
            }
            set {
                if ((this.CreatedUserRefField.Equals(value) != true)) {
                    this.CreatedUserRefField = value;
                    this.RaisePropertyChanged("CreatedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public System.DateTime CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public int ModifiedUserRef {
            get {
                return this.ModifiedUserRefField;
            }
            set {
                if ((this.ModifiedUserRefField.Equals(value) != true)) {
                    this.ModifiedUserRefField = value;
                    this.RaisePropertyChanged("ModifiedUserRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public System.DateTime ModifiedDate {
            get {
                return this.ModifiedDateField;
            }
            set {
                if ((this.ModifiedDateField.Equals(value) != true)) {
                    this.ModifiedDateField = value;
                    this.RaisePropertyChanged("ModifiedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public System.Nullable<System.Guid> SessionRef {
            get {
                return this.SessionRefField;
            }
            set {
                if ((this.SessionRefField.Equals(value) != true)) {
                    this.SessionRefField = value;
                    this.RaisePropertyChanged("SessionRef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public Pars.Util.Puma.UI.Mvc.ParsMail.MailQueueAttachmentDC[] MailQueueAttachment {
            get {
                return this.MailQueueAttachmentField;
            }
            set {
                if ((object.ReferenceEquals(this.MailQueueAttachmentField, value) != true)) {
                    this.MailQueueAttachmentField = value;
                    this.RaisePropertyChanged("MailQueueAttachment");
                }
            }
        }

        public override int ObjectRef
        {
            get { return 0; }
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ParsMail.IEmailService")]
    public interface IEmailService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/SendMail", ReplyAction="http://tempuri.org/IEmailService/SendMailResponse")]
        void SendMail(Pars.Util.Puma.UI.Mvc.ParsMail.MailSendRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/SendMail", ReplyAction="http://tempuri.org/IEmailService/SendMailResponse")]
        System.Threading.Tasks.Task SendMailAsync(Pars.Util.Puma.UI.Mvc.ParsMail.MailSendRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/TestSendMail", ReplyAction="http://tempuri.org/IEmailService/TestSendMailResponse")]
        void TestSendMail(string to, string subject, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/TestSendMail", ReplyAction="http://tempuri.org/IEmailService/TestSendMailResponse")]
        System.Threading.Tasks.Task TestSendMailAsync(string to, string subject, string body);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEmailServiceChannel : Pars.Util.Puma.UI.Mvc.ParsMail.IEmailService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmailServiceClient : System.ServiceModel.ClientBase<Pars.Util.Puma.UI.Mvc.ParsMail.IEmailService>, Pars.Util.Puma.UI.Mvc.ParsMail.IEmailService {
        
        public EmailServiceClient() {
        }
        
        public EmailServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SendMail(Pars.Util.Puma.UI.Mvc.ParsMail.MailSendRequest request) {
            base.Channel.SendMail(request);
        }
        
        public System.Threading.Tasks.Task SendMailAsync(Pars.Util.Puma.UI.Mvc.ParsMail.MailSendRequest request) {
            return base.Channel.SendMailAsync(request);
        }
        
        public void TestSendMail(string to, string subject, string body) {
            base.Channel.TestSendMail(to, subject, body);
        }
        
        public System.Threading.Tasks.Task TestSendMailAsync(string to, string subject, string body) {
            return base.Channel.TestSendMailAsync(to, subject, body);
        }
    }
}
