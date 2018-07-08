using Converter.Interfaces;
using Converter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Converter
{
    public class FileManager : IFileManager
    {
        public List<ServiceIndustry> GetServicesJson(string path)
        {
            var json = File.ReadAllText(path);
            var serviceList = JsonConvert.DeserializeObject<List<ServiceIndustry>>(json);
            return serviceList;
        }
        public List<ServiceIndustry> GetServicesXml(string path)
        {
            List<ServiceIndustry> services = new List<ServiceIndustry>();
            services = (
                from e in XDocument.Load(path).
                              Root.Elements("service")
                select new ServiceIndustry
                {
                    ServiceIndustryId = (int)e.Element("serviceId"),
                    Name = (string)e.Element("name"),
                    Executors = (
                            from o in e.Elements("executor").Elements("ex")
                            select new Executor
                            {
                                DateTime = (DateTime)o.Element("dateTime"),
                                Price = (double)o.Element("price"),
                                User = new List<User>(from phn in e.Descendants("user")
                                                      select new User
                                                      {
                                                          UserId = (int)phn.Element("userId"),
                                                          FirstName = (string)phn.Element("firstName"),
                                                          LastName = (string)phn.Element("lastName"),
                                                          Username = (string)phn.Element("userName"),
                                                          Email = (string)phn.Element("email"),
                                                          PhoneNumber = (string)phn.Element("phoneNumber")

                                                      })[0]
                            })
                            .ToList(),
                    Description = (string)e.Element("description")

                })
                    .ToList();
            return services;
        }

    }
}
