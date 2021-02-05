using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Util.Puma.ESB.Api.Models;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml;
using Pars.Util.Puma.ESB.Api.Controllers;

namespace Pars.Util.Puma.ESB.Api.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetXsd()
        {
            
            var result = DataContractHelper.WriteXmlSchemaForType<StaffAppointment>(".\\StaffAppointment.xsd");
        }

        //[TestMethod]
        //public void PhoneFormatTest()
        //{
        //    EsbController controller = new EsbController();
        //    var a = controller.FormatPhoneNumber("0090 (555) 1234567");
        //    Assert.IsTrue(!a.Mobile.Contains("("));
        //}
        [TestMethod]
        public void PhoneFormatTest()
        {
            EsbController controller = new EsbController();
            var user = new HarmonyUserParent { HarmonyUser = new HarmonyUser() { HrPersonelRef = 1045, Email = "kutlay.kunbi@lcwaikiki.com" } };
            controller.ResiningUser(user);
            
        }
    }
    public static class DataContractHelper
    {
        private static XmlWriterSettings xmlWriterSettings;

        private static XmlWriterSettings GetSettings()
        {
            if (DataContractHelper.xmlWriterSettings == null)
            {
                DataContractHelper.xmlWriterSettings = new XmlWriterSettings();
                DataContractHelper.xmlWriterSettings.Encoding = Encoding.UTF8;
                DataContractHelper.xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
                DataContractHelper.xmlWriterSettings.Indent = true;
            }
            return DataContractHelper.xmlWriterSettings;
        }

        public static string Serialize(object obj)
        {
            string empty = string.Empty;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType());
            //StringWriter stringWriter = new StringWriter();            
            //XmlWriter xmlWriter = XmlWriter.Create(stringWriter, DataContractHelper.GetSettings());
            //dataContractSerializer.WriteObject(xmlWriter, obj);
            //xmlWriter.Flush();
            //xmlWriter.Close();
            //return stringWriter.ToString();
            var ms = new MemoryStream();
            var xmlWriter = XmlWriter.Create(ms, DataContractHelper.GetSettings());
            dataContractSerializer.WriteObject(xmlWriter, obj);
            xmlWriter.Flush();
            xmlWriter.Close();
            ms.Position = 0;
            var sr = new StreamReader(ms, true);
            var xmlString = sr.ReadToEnd();
            ms.Flush();
            ms.Close();
            return xmlString;
        }

        public static void WriteAllSchemas<T>(Stream stream)
        {
            XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
            if (xsdDataContractExporter.CanExport(typeof(T)))
            {
                xsdDataContractExporter.Export(typeof(T));
                foreach (System.Xml.Schema.XmlSchema xmlSchema in xsdDataContractExporter.Schemas.Schemas())
                {
                    xmlSchema.Write(stream);
                }
            }
        }

        public static void WriteSchema<T>(Stream stream)
        {
            XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
            if (xsdDataContractExporter.CanExport(typeof(T)))
            {
                xsdDataContractExporter.Export(typeof(T));

                var xmlQualifiedName = xsdDataContractExporter.GetRootElementName(typeof(T));
                var targetNamespace = xmlQualifiedName.Namespace;

                foreach (System.Xml.Schema.XmlSchema xmlSchema in xsdDataContractExporter.Schemas.Schemas(targetNamespace))
                {
                    xmlSchema.Write(stream);
                }
            }
        }

        public static string GetHash<T>(this object instance) where T : HashAlgorithm, new()
        {
            T cryptoServiceProvider = new T();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
        {
            var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, instance);
            byte[] memoryStreamAsArray = stream.ToArray();
            byte[] memoryStreamHash = cryptoServiceProvider.ComputeHash(memoryStreamAsArray);

            string memoryStreamHashString = string.Empty;
            foreach (byte x in memoryStreamHash)
            {
                memoryStreamHashString += string.Format("{0:x2}", x);
            }

            return memoryStreamHashString.ToUpper();
        }

        //public static decimal? ToDecimal(this object obj)
        //{
        //    decimal outValue;
        //    if (!decimal.TryParse(obj.ToString(), out outValue))
        //        return null;

        //    return outValue;
        //}

        public static XmlSchemaWriteResult WriteXmlSchemaForType<T>(string fileName)
        {
            var ms = new MemoryStream();
            DataContractHelper.WriteSchema<T>(ms);
            ms.Position = 0;
            var sr = new StreamReader(ms, true);
            var xsd = sr.ReadToEnd();
            ms.Flush();
            ms.Close();

            var isOk = true;
            var assertMessage = string.Empty;

            try
            {
                var content = Encoding.UTF8.GetBytes(xsd);
                File.WriteAllBytes(fileName, content);
            }
            catch (Exception ex)
            {
                assertMessage = string.Format("{0} could not be created! Error has occured: {1}", fileName, ex.Message);
                isOk = false;
            }

            var result = new XmlSchemaWriteResult { IsOk = isOk, Message = assertMessage };

            return result;
        }
    }
    public class XmlSchemaWriteResult
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
    }
}
