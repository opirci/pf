using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Core;

//namespace Pars.Core.Service
namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public abstract class ResponseBase
    {
        /// <summary>Initializes a new instance of the <see cref="ResponseBase" /> class.</summary>
        public ResponseBase()
        {
            ErrorMessages = new List<string>();
            WarningMessages = new List<string>();
        }

        //TODO: ErrorMessages and WarningMessages nice to be ReadOnlyCollection instead of list. 
        //However WCF serialization problems risk to be solved

        /// <summary>Error messages to be returned.</summary>
        [DataMember(Name = "errorMessages")]
        public List<string> ErrorMessages { get; set; }

        [DataMember(Name = "warningMessages")]
        /// <summary>Warning messages to be returned.</summary>
        public List<string> WarningMessages { get; set; }


        /// <summary>True if response has no error messages.</summary>
        //[DataMember(Name = "succeeded")]
        public bool Succeeded
        {
            get { return ErrorMessages.Count == 0; }
        }

        /// <summary>True if response has warning messages.</summary>
        //[DataMember(Name = "hasWarnings")]
        public bool HasWarnings
        {
            get { return WarningMessages.Count != 0; }
        }

        /// <summary>Add the message</summary>
        /// <param key="message">The message.</param>
        /// <returns>ResponseBase for fluent purposes</returns>
        [Obsolete("Use AddError method instead.")]
        public ResponseBase AddMessage(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            ErrorMessages.Add(message);

            return this;
        }

        /// <summary>Add the error message</summary>
        /// <param name="message">The message.</param>
        /// <returns>ResponseBase for fluent purposes</returns>
        public ResponseBase AddError(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            ErrorMessages.Add(message);
            IReadOnlyCollection<string> ro = ErrorMessages.AsReadOnly();

            return this;
        }

        /// <summary>Add the warning message</summary>
        /// <param key="message">The message.</param>
        /// <returns>ResponseBase for fluent purposes</returns>
        public ResponseBase AddWarning(string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            WarningMessages.Add(message);

            return this;
        }

        public static T Execute<T>(Action<T> action) where T : ResponseBase, new()
        {
            T response = new T();
            try
            {
                action(response);
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return (response);
        }
    }


    [DataContract]
    public abstract class ResponseBase<T> : ResponseBase
    {
        public T Value { get; set; }

        public ResponseBase(T value) : base()
        {
            this.Value = value;
        }
    }
}
