using Converter.Interfaces;
using Converter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Converter
{
    public class ConverterManager : IConverterManager
    {
        FileManager fileManager = new FileManager();
        public void JsonToXmlServiceConverter()
        {
            var serviceIndustriesJson = fileManager.GetServicesJson(FilePaths.pathToServiceJson);
            string fileName = "services.xml";
            using (XmlWriter writer = XmlWriter.Create(FilePaths.jsonToXmlFolder + fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("services");

                foreach (ServiceIndustry service in serviceIndustriesJson)
                {
                    writer.WriteStartElement("service");

                    writer.WriteElementString("serviceIndustryId", service.ServiceIndustryId.ToString());
                    writer.WriteElementString("name", service.Name.ToString());
                    writer.WriteStartElement("executor");
                    foreach (Executor executor in service.Executors)
                    {
                        writer.WriteStartElement("ex");
                        writer.WriteElementString("dateTime", executor.DateTime.ToString());
                        writer.WriteElementString("price", executor.Price.ToString());
                        writer.WriteStartElement("user");
                        writer.WriteElementString("userId", executor.User.UserId.ToString());
                        writer.WriteElementString("firstName", executor.User.FirstName.ToString());
                        writer.WriteElementString("lastName", executor.User.LastName.ToString());
                        writer.WriteElementString("username", executor.User.Username.ToString());
                        writer.WriteElementString("email", executor.User.Email.ToString());
                        writer.WriteElementString("phoneNumber", executor.User.PhoneNumber.ToString());
                        writer.WriteEndElement();
                        writer.WriteEndElement();

                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("description", service.Description.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void XmlToJsonServiceConverter()
        {
            var serviceIndustriesXml = fileManager.GetServicesXml(FilePaths.pathToServiceXml);
            string fileName = "services.json";
            using (StreamWriter file = File.CreateText(FilePaths.xmlToJsonFolder + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, serviceIndustriesXml);
            }
        }
    }
}
